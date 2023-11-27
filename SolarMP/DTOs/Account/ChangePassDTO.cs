namespace SolarMP.DTOs.Account
{
    public class ChangePassDTO
    {
        public string accountId { get; set; }
        public string oldPass { get; set; }
        public string newPass { get; set; }
    }
}
