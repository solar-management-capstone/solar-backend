using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Feedback
{
    public class FeedbackCreateDTO
    {
        public string Description { get; set; }
        public string ContructionContractId { get; set; }
        public string AccountId { get; set; }
        public string? Image { get; set; }
        public string PackageId { get; set; }
    }
}
