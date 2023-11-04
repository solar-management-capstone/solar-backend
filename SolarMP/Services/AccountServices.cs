using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Account;
using SolarMP.DTOs.Account.Team;
using SolarMP.Interfaces;
using SolarMP.Models;
using Twilio.Types;

namespace SolarMP.Services
{
    public class AccountServices : IAccount
    {
        protected readonly solarMPContext context;
        private readonly ITwilio twilio;
        public AccountServices(solarMPContext context, ITwilio twilio)
        {
            this.context = context;
            this.twilio = twilio;
        }

        public async Task<bool> addTeam(TeamDTO dto)
        {
            var ListFinal = new List<string>();
            try
            {
                var delete = await this.context.Team.Where(x=>x.StaffLeadId.Equals(dto.LeaderId)).ToListAsync();
                this.context.Team.RemoveRange(delete);
                await this.context.SaveChangesAsync();

                var checkLead = await this.context.Account.Where(x => x.AccountId.Equals(dto.LeaderId) && x.IsLeader == true && x.RoleId == "3")
                    .FirstOrDefaultAsync();
                if (checkLead == null)
                {
                    throw new Exception("Staff này không phải leader");
                }
                foreach(var x in dto.member)
                {
                    var checkMem = await this.context.Account.Where(x=>x.AccountId.Equals(x.AccountId) && x.IsLeader == false && x.RoleId == "3" && x.Status)
                        .FirstOrDefaultAsync();
                    if(checkMem == null)
                    {
                        throw new Exception("tài khoản " + checkMem.AccountId + "không phù hợp để thêm vào nhóm");
                    }

                    var check = await this.context.Team.Where(x => x.StaffId.Equals(x.StaffId) && x.Status).FirstOrDefaultAsync();
                    if (check != null)
                    {
                        throw new Exception("tài khoản " + checkMem.AccountId + "đã có nhóm");
                    }

                    var team = new Team();
                    team.StaffLeadId = dto.LeaderId;
                    team.StaffId = x.memberId;
                    team.Status = true;
                    team.CreateAt = DateTime.Now;
                    await this.context.Team.AddAsync(team);
                    await this.context.SaveChangesAsync();

                    ListFinal.Add(x.memberId);
                }

                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                foreach(var a in ListFinal)
                {
                    var check = await this.context.Team.Where(x=>x.StaffLeadId.Equals(dto.LeaderId) && x.StaffId.Equals(a.ToString()))
                        .FirstOrDefaultAsync();
                    this.context.Team.Remove(check);
                    await this.context.SaveChangesAsync();
                }
            }
        }

        public async Task<Account> delete(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Account.Update(check);
                    await this.context.SaveChangesAsync();
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

        public async Task<Account> deleteHardCode(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    this.context.Account.Remove(check);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE"))
                {
                    throw new Exception("data còn ở các bảng khác nên không xóa được đâu!");
                }
                throw new Exception("Lỗi gòi");
            }
        }

        public async Task<Team> deleteMember(string leadId, string memberId)
        {
            try
            {
                var check = await this.context.Team.Where(x=>x.StaffLeadId.Equals(leadId) && x.StaffId.Equals(memberId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Team.Update(check);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Account>> getAll()
        {
            try
            {
                var account = await this.context.Account
                    .Include(x=>x.Role)
                    .ToListAsync();
                return account;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Team>> getAllMember()
        {
            try
            {
                var check = await this.context.Team
                    .Include(x=>x.StaffLead)
                    .Include(x=>x.Staff)
                    .ToListAsync();
                return check;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> getById(string id)
        {
            try
            {
                var check = await this.context.Account.Where(x=>x.AccountId.Equals(id)).FirstOrDefaultAsync();
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

        public async Task<List<Account>> getByName(string name)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.Firstname.Contains(name) || x.Lastname.Contains(name)).ToListAsync();
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

        public async Task<List<Team>> getMemberStaff(string leadId)
        {
            try
            {
                var check = await this.context.Team.Where(x=>x.StaffLeadId.Equals(leadId))
                    .Include(x => x.StaffLead)
                    .Include(x => x.Staff)
                    .ToListAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> register(AccountRegisterDTO dto)
        {
            try
            {
                var account = new Account();
                account.AccountId = "ACC"+Guid.NewGuid().ToString().Substring(0,13);
                account.RoleId = dto.RoleId ?? "4";
                account.Phone = dto.Phone;
                account.Email = dto.Email;
                account.Username = dto.Username;
                account.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                account.Address = dto.Address;
                account.Gender= dto.Gender;
                account.Firstname = dto.Firstname;
                account.Lastname = dto.Lastname;
                account.Status = true;
                account.CreateAt = DateTime.Now;
                account.IsGoogleProvider = dto.IsGoogleProvider;
                account.IsLeader = false;
                account.IsFree = true;

                string digitsOnly = new string(dto.Phone.Where(char.IsDigit).ToArray());

                if (!digitsOnly.StartsWith("+84"))
                {
                    digitsOnly = "+84" + digitsOnly;
                }

                if (account.RoleId == "4")
                {
                    account.RoleId = "4";
                    account.Status = false;
                }
                if (account.RoleId == "3")
                {
                    account.RoleId = "3";
                    account.IsLeader = dto.IsLeader ?? null;
                }
                await this.context.Account.AddAsync(account);
                await this.context.SaveChangesAsync();

                twilio.SendOTP(digitsOnly);

                return account;
            }
            catch(Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    if (ex.InnerException.Message.Contains("User"))
                    {
                        throw new Exception("Tên đăng nhập đã tồn tại");
                    }
                    if (ex.InnerException.Message.Contains("email"))
                    {
                        throw new Exception("Email đã tồn tại");
                    }
                    if (ex.InnerException.Message.Contains("phone"))
                    {
                        throw new Exception("Số điện thoại đã tồn tại");
                    }
                }
                throw new Exception("Lỗi đăng ký");
            }
        }

        public async Task<List<Account>> search(string? name, string? phone, string? email)
        {
            try
            {
                var check = await this.context.Account
                    .Where(x=>x.IsLeader == false && (x.Firstname.Contains(name) || x.Phone.Contains(phone) || x.Email.Contains(email)))
                    .Include(x=>x.TeamStaff.Where(x=> !x.Status))
                    .ToListAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> update(AccountUpdateDTO dto)
        {
            try
            {
                var check = await this.context.Account.Where(x => x.AccountId.Equals(dto.AccountId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Address = dto.Address ?? check.Address;
                    check.Gender = dto.Gender ?? check.Gender;
                    check.Firstname= dto.Firstname ?? check.Firstname;
                    check.Lastname= dto.Lastname ?? check.Lastname;
                    if(dto.Password!= null)
                    {
                        check.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                    }         
                    check.IsGoogleProvider = dto.IsGoogleProvider ?? check.IsGoogleProvider;
                    check.Status = dto.Status ?? check.Status;
                    check.IsLeader = dto.IsLeader ?? check.IsLeader;
                    check.RoleId = dto.RoleId ?? check.RoleId;
                    this.context.Account.Update(check);
                    this.context.SaveChangesAsync();
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
    }
}
