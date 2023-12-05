using SolarMP.Models;

namespace SolarMP.DTOs.Account.Team
{
    public class TeamRes
    {
        public SolarMP.Models.Account staffLead { get; set; }
        public List<SolarMP.Models.Account> staff { get; set; }
    }
}
