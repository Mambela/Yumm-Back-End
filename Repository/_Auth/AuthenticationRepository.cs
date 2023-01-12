using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yumm_Back_End.Configuration;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Repository._ClientRepository;
using Yumm_Back_End.Repository._Restaurant;

namespace Yumm_Back_End.Repository._Auth
{
    public class AuthenticationRepository : IAuthRepository
    {

        public UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public readonly ApplicationDbContext _dbContext;

        public readonly AppSettings _appSettings;

        public IConfiguration _configuration;
        public IRestaurantRepository _restaurantRepo;
        public IClientRepository _clientRepo;



        public AuthenticationRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IRestaurantRepository restaurantRepo, ApplicationDbContext dbContext, IOptions<AppSettings> appSettings, IClientRepository clientRepository)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _restaurantRepo = restaurantRepo;
            _clientRepo = clientRepository;
            _appSettings = appSettings.Value;


        }

        public bool RegisterDriver()
        {
            throw new NotImplementedException();
        }

        public bool RegisterClient()
        {
            throw new NotImplementedException();
        }



        #region Authentication 
        #region Restaurant
        public async Task<AuthenticatedUser> AuthenticateRestaurant(string password, string email = null, string phoneNumber = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            string currentRole = "";



            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                //Inside here it means that the user has been found!!!!


                //fetch the Data of the Restaurant
                Restaurant restaurant = _dbContext
                    .Restaurants.Single();

                Console.WriteLine("\n\n Restaurant Name " + restaurant.Name);

                // fetch all the roles associate to this user
                var userRoles = await _userManager.GetRolesAsync(user);


                //the claims that will be added to the token when it gets created
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    currentRole = userRole;

                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);


                //return the Restaurant data to the user
                AuthenticatedUser authUser = new AuthenticatedUser
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Token = tokenHandler.WriteToken(token),
                    role = currentRole,
                    EmailVerified = user.EmailConfirmed,
                    PhoneVerified = user.PhoneNumberConfirmed

                };


                Console.WriteLine(userRoles);

                return authUser;
            }


            return null;


        }
        //#endregion

        #region Client
        public async Task<AuthenticatedUser> AuthenticatedClient(string password, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            string currentRole = null;
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                //INside here it means that the user is found 


                //fetch the data required

                Client client = _dbContext.Clients.Single(c => c.UserId == user.Id);


                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    currentRole = userRole;
                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                AuthenticatedUser authUser = new AuthenticatedUser
                {
                    Id = client.Id,
                    Name = client.FirstName,
                    Surname = client.LastName
                    ,
                    Token = tokenHandler.WriteToken(token),
                    role = currentRole

                };

                return authUser;
            }

            return null;
        }

        #endregion

        #region Drivers
        #endregion

        #region Support Team
        #endregion

        #endregion

        #region Registration 




        public async Task<bool> RegisterRestaurantAsync(string Email, string password, string phoneNumber, string name, string Description, int RestaurantId,string token)
        {

            ApplicationUser user = new ApplicationUser
            {
                Email = Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Email,
                PhoneNumber = phoneNumber,
                Token = token

            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            if (!await _roleManager.RoleExistsAsync(UserRoles.RESTAURANT))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.RESTAURANT));

            if (await _roleManager.RoleExistsAsync(UserRoles.RESTAURANT))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.RESTAURANT);
            }

            Restaurant restaurant = new Restaurant
            {
                Name = name,
                Description = Description,
               // UserId = user.Id,
                RestaurantTypeId = RestaurantId
            };

            return await _restaurantRepo.CreateRestaurant(restaurant);

        }

        public bool RegisterSupportTeam()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterClientAsync(RegisterClientModel model)
        {
            try
            {

                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var clientResult = await _userManager.CreateAsync(user, model.Password);

                if (!clientResult.Succeeded)
                    return false;

                if (!await _roleManager.RoleExistsAsync(UserRoles.CLIENT))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.CLIENT));

                if (await _roleManager.RoleExistsAsync(UserRoles.CLIENT))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.CLIENT);
                }

                Client client = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    UserId = user.Id
                };

                return await _clientRepo.CreateClient(client);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ApplicationUser GetUserByPhone(string phoneNumber)
        {
            return  _dbContext.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public ApplicationUser GetUserByUserName(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserName == username);
        }


        #endregion

        #endregion
    }
}
