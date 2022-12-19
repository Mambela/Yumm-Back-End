using System;
using Yumm_Back_End.Models;

namespace Yumm_Back_End.Helper
{
    public class TokenManager
    {

        string GenerateEmailVerificationToken()
        {
            return null;
        }

        public OTPModel GenerateOTP()
        {
            try
            {
                int otpToken = 0;
                Random rnd = new Random();
                var now = DateTime.UtcNow.AddMinutes(3);
                otpToken = rnd.Next(1000, 9999);

                OTPModel otpModel = new OTPModel
                {
                    ExpirationDate = now,
                    OTP = otpToken
                };


                return otpModel;
            }
            catch (System.Exception )
            {

                throw;
            }
        }
    }
}
