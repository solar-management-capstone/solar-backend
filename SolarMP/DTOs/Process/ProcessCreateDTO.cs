using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarMP.DTOs.Image;

namespace SolarMP.DTOs.Process
{
    public class ProcessCreateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ContractId { get; set; }
        public List<ImageDTO>? image { get; set; }

    }
}
