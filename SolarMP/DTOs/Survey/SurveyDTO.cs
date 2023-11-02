using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Survey
{
    public class SurveyDTO
    {
        public string Description { get; set; }
        public string Note { get; set; }
        public string StaffId { get; set; }
    }
}
