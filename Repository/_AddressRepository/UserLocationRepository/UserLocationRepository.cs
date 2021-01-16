using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;

namespace Yumm_Back_End.Repository._AddressRepository.UserLocationRepository
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private ApplicationDbContext _dbContext;

        public UserLocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<bool> CreateUserLocations(UsersLocations usersLocation)
        {
            await _dbContext.UsersLocations.AddAsync(usersLocation);

            return Save();
            
        }

        public bool DeleteUserLocation(UsersLocations userLocations)
        {
            _dbContext.UsersLocations.Remove(userLocations);

            return Save();
        }

        public ICollection<UsersLocations> GetAllUserLocations(string userId)
        {
            
            return _dbContext.UsersLocations.Include(u => u.Location).Where(ul => ul.UserId == userId).ToList();
        }

        public UsersLocations GetUserLocation(string userId, int locationId)
        {
            return _dbContext.UsersLocations.FirstOrDefault(l => l.LocationId == locationId && l.UserId == userId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateUserLocation(UsersLocations usersLocations)
        {
            _dbContext.UsersLocations.Update(usersLocations);

            return Save();

        }
    }
}
