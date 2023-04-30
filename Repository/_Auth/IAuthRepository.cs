using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Repository._Auth
{
    public interface IAuthRepository
    {

        bool RegisterDriver();

        bool RegisterClient();

        Task<bool> RegisterRestaurantAsync(string Email, string password, string phoneNumber, string name, string Description,int RestaurantId,string token);
        Task<AuthenticatedUser> AuthenticateRestaurant(string password,string email = null, string phoneNumber = null );

        Task<AuthenticatedUser> AuthenticatedClient(string password, string email);

        Task<bool> RegisterClientAsync(RegisterClientModel clientModel);

        RestaurantUser getUser(string userId);

        bool RegisterSupportTeam();
       ApplicationUser GetUserByPhone(string phoneNumber);

        ApplicationUser GetUserByUserName(string username);
    }
}
