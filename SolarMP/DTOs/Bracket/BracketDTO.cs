using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarMP.DTOs.Image;

namespace SolarMP.DTOs.Bracket
{
    public class BracketDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Manufacturer { get; set; }
        public List<ImageDTO>? image { get; set; }
    }
}
