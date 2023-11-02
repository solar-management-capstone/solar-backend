using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Promotions;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class PromotionService : IPromotion
    {
        protected readonly solarMPContext context;
        public PromotionService(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeletePromotion(string promotionId)
        {
            try
            {
                var promotion = await this.context.Promotion
                    .Where(x => promotionId.Equals(x.PromotionId))
                    .FirstOrDefaultAsync();
                if (promotion != null)
                {
                    promotion.Status = false;
                    this.context.Promotion.Update(promotion);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Promotion found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Promotion>> GetAllPromotions()
        {
            try
            {
                var data = await this.context.Promotion
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Promotion>> GetPromotionById(string? promotionId)
        {
            try
            {
                var data = await this.context.Promotion.Where(x => x.Status && x.PromotionId.Equals(promotionId)).ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> InsertPromotion(PromotionDTO promotion)
        {
            try
            {
                var _promotion = new Promotion();
                _promotion.PromotionId = "PROMO" + Guid.NewGuid().ToString().Substring(0, 11);
                _promotion.Amount= promotion.Amount;
                _promotion.Title = promotion.Title;
                _promotion.Description = promotion.Description;
                _promotion.StartDate = promotion.StartDate;
                _promotion.EndDate = promotion.EndDate;
                _promotion.CreateAt= DateTime.Now;
                _promotion.Status = true;
                await this.context.Promotion.AddAsync(_promotion);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdatePromotion(PromotionUpdateDTO upPromotion)
        {
            try
            {
                Promotion promotion = await this.context.Promotion.FirstAsync(x => x.PromotionId == upPromotion.PromotionId);
                if (promotion != null)
                {
                    promotion.Amount = upPromotion.Amount ?? promotion.Amount;
                    promotion.Title = upPromotion.Title ?? promotion.Title ;
                    promotion.Description = upPromotion.Description ?? promotion.Description;
                    promotion.StartDate = upPromotion.StartDate ?? promotion.StartDate;
                    promotion.EndDate = upPromotion.EndDate ?? promotion.EndDate;
                    promotion.Status = upPromotion.Status ?? promotion.Status;
                    context.Promotion.Update(promotion);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Promotion not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
