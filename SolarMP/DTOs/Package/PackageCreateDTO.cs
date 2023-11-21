using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarMP.DTOs.Package
{
    public class PackageCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PromotionId { get; set; }
        public int? RoofArea { get; set; }
        public decimal? ElectricBill { get; set; }
        public string? PresentImage { get; set; }
    }
}
