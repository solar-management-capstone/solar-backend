namespace SolarMP.DTOs.Promotions
{
    public class PromotionUpdateDTO
    {
        public string PromotionId { get; set; }
        public decimal? Amount { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }
    }
}
