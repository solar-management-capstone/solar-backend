
namespace SolarMP.DTOs.Acceptances
{
    public class AcceptanceUpdateDTO
    {
        public string? AcceptanceId { get; set; }
        public bool? Status { get; set; }
        public int? Rating { get; set; }
        public string? Feedback { get; set; }
        public string? ConstructionContractId { get; set; }
        public string? ImageFile { get; set; }
    }
}
