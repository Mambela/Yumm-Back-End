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
    }
}
