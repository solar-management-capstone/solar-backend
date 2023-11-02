using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarMP.DTOs.Image;

namespace SolarMP.DTOs.Product
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Manufacturer { get; set; }
        public string Feature { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public List<ImageDTO>? image { get; set; }
    }
}
