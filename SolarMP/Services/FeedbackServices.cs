using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Feedback;
using SolarMP.DTOs.Package;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class FeedbackServices : IFeedback
    {
        protected readonly solarMPContext context;
        public FeedbackServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<Feedback> delete(string id)
        {
            try
            {
                var check = await this.context.Feedback.Where(x => x.FeedbackId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Feedback.Update(check);
                    await this.context.SaveChangesAsync();

                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Feedback>> getAll()
        {
            try
            {
                var check = await this.context.Feedback
                    .Include(x=>x.Account)
                    .Include(x=>x.ContructionContract)
                    .Include(x=>x.Package)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Feedback>> getAllPack(string packageId)
        {
            try
            {
                var check = await this.context.Feedback
                    .Where(x=>x.PackageId.Equals(packageId))
                    .Include(x => x.Account)
                    .Include(x => x.ContructionContract)
                    .Include(x => x.Package)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Feedback> insert(FeedbackCreateDTO dto)
        {
            try
            {
                var feedback = new Feedback();
                feedback.PackageId = dto.PackageId;
                feedback.Status = true;
                feedback.CreateAt = DateTime.Now;
                feedback.Rating = dto.Rating;
                feedback.AccountId = dto.AccountId;
                feedback.Description = dto.Description;
                feedback.Image= dto.Image ?? "none";
                feedback.ContructionContractId = dto.ContructionContractId;
                feedback.FeedbackId = "FEBK" + Guid.NewGuid().ToString().Substring(0, 12);

                await this.context.Feedback.AddAsync(feedback);
                await this.context.SaveChangesAsync();
                return feedback;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
