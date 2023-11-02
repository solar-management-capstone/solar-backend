using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarMP.DTOs.Image;

namespace SolarMP.DTOs.WarrantyReport
{
    public class WarrantyReportDTO
    {
        public string Manufacturer { get; set; }
        public string Feature { get; set; }
        public string Description { get; set; }
        public string ContractId { get; set; }
        public string AccountId { get; set; }
        public List<ImageDTO>? image { get; set; }

    }
}
