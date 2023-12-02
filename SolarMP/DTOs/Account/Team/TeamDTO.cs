namespace SolarMP.DTOs.Account.Team
{
    public class TeamDTO
    {
        public string? LeaderId { get; set; }
        public string? newLeaderId { get; set; }
        public List<MemberDTO>? member { get; set; }
    }
}
