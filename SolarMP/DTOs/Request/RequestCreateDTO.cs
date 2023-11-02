using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Request
{
    public class RequestCreateDTO
    {
        public string PackageId { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
    }
}
