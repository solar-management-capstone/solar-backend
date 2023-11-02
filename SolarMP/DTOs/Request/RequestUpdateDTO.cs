using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Request
{
    public class RequestUpdateDTO
    {
        public string RequestId { get; set; }
        public string StaffId { get; set; }
    }
}
