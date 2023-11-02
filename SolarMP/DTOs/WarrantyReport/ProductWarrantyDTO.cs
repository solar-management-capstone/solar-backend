namespace SolarMP.DTOs.WarrantyReport
{
    public class ProductWarrantyDTO
    {
        public string WarrantyId { get; set; }
        public List<ProductDamage> damages { get; set; }
    }
}
