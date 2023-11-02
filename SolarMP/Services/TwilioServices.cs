using SolarMP.Interfaces;
using Twilio.Rest.Verify.V2.Service;
using Twilio;

namespace SolarMP.Services
{
    public class TwilioServices : ITwilio
    {
        private readonly IConfiguration _configuration;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _pathServiceSid;
        public TwilioServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            _accountSid = _configuration.GetValue<string>("twillio:accountsid");
            _authToken = _configuration.GetValue<string>("twillio:authtoken");
            _pathServiceSid = _configuration.GetValue<string>("twillio:pathservicesid");
        }
        public async Task<DateTime?> SendOTP(string phoneNumber)
        {
            try
            {
                TwilioClient.Init(_accountSid, _authToken);

                var verification = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: "sms",
                    pathServiceSid: _pathServiceSid,
                    locale: "vi"
                    );

                return verification.DateCreated;
            }catch ( Exception ex )
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<string> VerifyOTP(string phoneNumber, string otp)
        {
            try
            {
                TwilioClient.Init(_accountSid, _authToken);

                var verificationResult = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: otp,
                    pathServiceSid: _pathServiceSid
                    );

                return verificationResult.Status.ToString();
            }
            catch ( Exception ex )
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
