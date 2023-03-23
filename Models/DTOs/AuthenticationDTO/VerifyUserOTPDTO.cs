namespace Yumm_Back_End.Models.DTOs.AuthenticationDTO
{
    public class VerifyUserOTPDTO
    {

        public int OTP { get; set; }

        public string phoneNumber { get; set; }
    }
}
