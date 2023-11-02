namespace SolarMP.DTOs.ConstructionContract
{
    public class ConstructionContractUpdateDTO
    {
        public string ConstructioncontractId { get; set; }
        public bool? Status { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public decimal? Totalcost { get; set; }
        public bool? IsConfirmed { get; set; }
        public string? ImageFile { get; set; }
        public string? CustomerId { get; set; }
        public string? Staffid { get; set; }
        public string? PackageId { get; set; }
        public string? BracketId { get; set; }
    }
}
