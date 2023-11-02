using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Process;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class ProcessServices : IProcess
    {
        protected readonly solarMPContext context;
        public ProcessServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<Process> delete(string id)
        {
            try
            {
                var check = await this.context.Process.Where(x => x.ProcessId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Process.Update(check);
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

        public async Task<List<Process>> getAll()
        {
            try
            {
                var check = await this.context.Process.Where(x => x.Status)
                    .Include(x=>x.Image)
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

        public async Task<List<Process>> getAllContract(string id)
        {
            try
            {
                var check = await this.context.Process.Where(x => x.Status && x.ContractId.Equals(id))
                    .Include(x => x.Image)
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

        public async Task<Process> getById(string id)
        {
            try
            {
                var check = await this.context.Process.Where(x => x.ProcessId.Equals(id))
                    .Include(x => x.Image)
                    .FirstOrDefaultAsync();
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

        public async Task<Process> insert(ProcessCreateDTO dto)
        {
            try
            {
                var process = new Process();
                process.ProcessId = "PRC" + Guid.NewGuid().ToString().Substring(0, 13);
                process.Title = dto.Title;
                process.Description = dto.Description;
                process.StartDate = (DateTime)dto.StartDate;
                process.EndDate = (DateTime)dto.EndDate;
                process.CreateAt = DateTime.Now;
                process.Status = true;
                process.ContractId = dto.ContractId;

                await this.context.Process.AddAsync(process);
                await this.context.SaveChangesAsync();

                if (dto.image != null && dto.image.Count > 0)
                {
                    foreach (var image in dto.image)
                    {
                        var img = new Image();
                        img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                        img.ImageData = image.image;
                        img.ProcessId = process.ProcessId;
                        await this.context.Image.AddAsync(img);
                        await this.context.SaveChangesAsync();
                    }
                }
                return process;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
