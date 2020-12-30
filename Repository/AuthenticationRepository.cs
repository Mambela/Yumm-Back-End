using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._Auth;
using Yumm_Back_End.Repository._Restaurant;

namespace Yumm_Back_End.Repository
{
    public class AuthenticationRepository : IAuthRepository
    {

        public UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public IConfiguration _configuration;
        public IRestaurantRepository _restaurantRepo;
 

       
        public AuthenticationRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, IRestaurantRepository restaurantRepo)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _restaurantRepo = restaurantRepo; 

        }


        public bool RegisterClient()
        {
            throw new NotImplementedException();
        }

        public bool RegisterDriver()
        {
            throw new NotImplementedException();
        }

        

        public async Task<bool> RegisterRestaurantAsync(string Email, string password, string phoneNumber, string name, string Description)
        {

            ApplicationUser user = new ApplicationUser
            {
                Email = Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Email,
                PhoneNumber = phoneNumber

            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            if (!await _roleManager.RoleExistsAsync(UserRoles.ADMIN))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));

            if (await _roleManager.RoleExistsAsync(UserRoles.ADMIN))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.ADMIN);
            }

            Restaurant restaurant = new Restaurant 
            {
                Name = name,
                Description = Description,
                UserId = user.Id
            };


            Console.WriteLine("Last inserted user" + user.Id);

            return _restaurantRepo.CreateRestaurant(restaurant);

        }

        public bool RegisterSupportTeam()
        {
            throw new NotImplementedException();
        }

      
    }
}
