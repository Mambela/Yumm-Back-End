using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Repository._Auth
{
    public interface IAuthRepository
    {

        bool RegisterDriver();

        bool RegisterClient();
        Task<bool> RegisterRestaurantAsync(string Email,string password, string phoneNumber,string name, string Description);

        bool RegisterSupportTeam();

        

    }
}
