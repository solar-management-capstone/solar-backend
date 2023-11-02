using SolarMP.DTOs.Promotions;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IPromotion
    {
        Task<List<Promotion>> GetPromotionById(string? promotionId);
        Task<List<Promotion>> GetAllPromotions();
        Task<bool> UpdatePromotion(PromotionUpdateDTO upPromotion);
        Task<bool> DeletePromotion(string promotionId);
        Task<bool> InsertPromotion(PromotionDTO promotion);
    }
}
