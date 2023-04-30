using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Repository._UserRepository
{
    public interface IUserRepository
    {

        ApplicationUser GetUser(string userId);

        public ApplicationUser GetVerifyUser(string Token);

        ApplicationUser GetUserToVerifyOTP(int OTP);

        public bool UpdateUser(ApplicationUser user);

        public ApplicationUser GetUserPhoneNumber(string phoneNumber);
        public ApplicationUser GetUserPhoneNumber(int OTP,string phoneNumber);

        public bool Save();
    }
}
