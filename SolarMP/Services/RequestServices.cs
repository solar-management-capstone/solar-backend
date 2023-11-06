using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Request;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class RequestServices : IRequest
    {
        protected readonly solarMPContext context;
        public RequestServices(solarMPContext context)
        {
            this.context = context;
        }

        public async Task<Request> assignStaff(RequestUpdateDTO dto)
        {
            try
            {
                var check = await this.context.Request.Where(x => x.RequestId.Equals(dto.RequestId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    var staff = await this.context.Account.Where(x => x.AccountId.Equals(dto.StaffId))
                        .Include(x => x.RequestStaff.Where(x => x.Status))
                        .FirstOrDefaultAsync();
                    if (staff.RequestStaff.Count >= 3)
                    {
                        throw new Exception("staff nhận 3 request");
                    }

                    check.StaffId= dto.StaffId;
                    this.context.Request.Update(check);
                    await this.context.SaveChangesAsync();
                    
                    if (staff.RequestStaff.Count ==3)
                    {
                        staff.IsFree = false;
                    }
                    staff.IsFree = true;
                    this.context.Account.Update(staff);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Request>> getAll()
        {
            try
            {
                var check = await this.context.Request
                    .Include(x => x.Staff)
                    .Include(x => x.Account)
                    .Include(x => x.Survey)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Request>> getAllForPackage(string id)
        {
            try
            {
                var check = await this.context.Request.Where(x => x.PackageId.Equals(id))
                    .Include(x => x.Staff)
                    .Include(x => x.Account)
                    .Include(x => x.Package)
                    .Include(x => x.Survey)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Request>> getAllForStaff(string id)
        {
            try
            {
                var check = await this.context.Request.Where(x => x.StaffId.Equals(id))
                    .Include(x => x.Staff)
                    .Include(x => x.Account)
                    .Include(x=>x.Package)
                    .Include(x => x.Survey)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Request>> getAllForUser(string id)
        {
            try
            {
                var check = await this.context.Request.Where(x => x.AccountId.Equals(id))
                    .Include(x => x.Staff)
                    .Include(x => x.Package)
                    .Include(x => x.Survey)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Request> insert(RequestCreateDTO dto)
        {
            try
            {
                var request = new Request();
                request.Status = true;
                request.CreateAt = DateTime.Now;
                request.Description = dto.Description;
                request.AccountId= dto.AccountId;
                request.PackageId= dto.PackageId;
                request.RequestId = "REQ" + Guid.NewGuid().ToString().Substring(0, 13);

                await this.context.Request.AddAsync(request);
                await this.context.SaveChangesAsync();

                return request;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Account>> searchStaffFree()
        {
            try
            {
                var check = await this.context.Account.Where(x=>x.Status && x.IsLeader == true)
                    .Include(x=>x.RequestStaff.Where(x=>x.Status))
                    .ToListAsync();
                if(check != null)
                {
                    return check;
                }
                return null;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Request> updateRequest(string id)
        {
            try
            {
                var check = await this.context.Request.Where(x => x.RequestId.Equals(id)).FirstOrDefaultAsync();
                if(check != null)
                {
                    check.Status = false;
                    this.context.Request.Update(check);
                    await this.context.SaveChangesAsync();

                    var staff = await this.context.Account.Where(x => x.AccountId.Equals(check.AccountId))
                    .Include(x => x.RequestStaff.Where(x => x.Status))
                    .FirstOrDefaultAsync();
                    if(staff == null)
                    {
                        staff.IsFree = true;
                        this.context.Account.Update(staff);
                        await this.context.SaveChangesAsync();
                    }
                    return check;
                }
                else
                {
                    return null;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
