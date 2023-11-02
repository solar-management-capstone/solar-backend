using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarMP.Models;

namespace SolarMP.DTOs.Acceptances
{
    public class AcceptanceDTO
    {
        public int? Rating { get; set; }
        public string? Feedback { get; set; }
        public string? ConstructionContractId { get; set; }
        public string? ImageFile { get; set; }
        
    }
}
