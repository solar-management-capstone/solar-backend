using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Package
{
    public class PackageUpdateDTO
    {
        public string PackageId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PromotionId { get; set; }
        public bool? Status { get; set; }
        public bool? IsDisablePromotion { get; set; } = false;
        public int? RoofArea { get; set; }
        public decimal? ElectricBill { get; set; }
        public string? PresentImage { get; set; }
    }
}
