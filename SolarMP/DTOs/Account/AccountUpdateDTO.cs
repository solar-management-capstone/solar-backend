using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Account
{
    public class AccountUpdateDTO
    {
        public string AccountId { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        public bool? IsGoogleProvider { get; set; } = false;
        public bool? IsLeader { get; set; }

    }
}
