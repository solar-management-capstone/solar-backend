namespace SolarMP.Interfaces
{
    public interface ITwilio
    {
        Task<DateTime?> SendOTP(string phoneNumber);
        Task<string> VerifyOTP(string phoneNumber, string otp);
    }
}
