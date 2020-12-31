using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Authentication;

namespace Yumm_Back_End.Repository._Auth
{
    public interface IAuthRepository
    {

        bool RegisterDriver();

        bool RegisterClient();
        Task<bool> RegisterRestaurantAsync(string Email, string password, string phoneNumber, string name, string Description);


        Task<AuthenticatedUser> AuthenticateRestaurant(string password,string email = null, string phoneNumber = null );
            


        bool RegisterSupportTeam();

        

    }
}
