namespace SolarMP.DTOs.ConstructionContract
{
    public class CusManyStatusDTO
    {
        public string customerId { get; set; }
        public List<StatusDTO> status { get; set; }
    }
}
