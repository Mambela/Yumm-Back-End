using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Repository._UserRepository
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext _dbContext;


        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ApplicationUser GetUser(string userId)
        {
            return _dbContext.Users.First(u => u.Id == userId);
        }

        public ApplicationUser GetUserPhoneNumber(string phoneNumber)
        {
            return _dbContext.Users.FirstOrDefault(p => p.PhoneNumber == phoneNumber);
        }

        public ApplicationUser GetUserToVerifyOTP(int OTP)
        {
            return _dbContext.Users.FirstOrDefault(t => t.OTP == OTP);
        }

        public ApplicationUser GetVerifyUser(string Token)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Token.ToLower() == Token.ToLower()); 
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateUser(ApplicationUser user)
        {
            _dbContext.Users.Update(user);
            return Save();
        }

    }
}
