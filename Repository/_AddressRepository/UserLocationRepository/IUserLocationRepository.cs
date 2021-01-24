using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.UserLocationRepository
{
    public interface IUserLocationRepository
    {

        Task<bool> CreateUserLocations( UsersLocations usersLocation);

        bool UpdateUserLocation(UsersLocations usersLocations);

        bool DeleteUserLocation(UsersLocations userLocations);

        ICollection<UsersLocations> GetAllUserLocations(string userId);

        UsersLocations GetUserLocation(string userId, int locationId);

        bool Save();


    }
}
