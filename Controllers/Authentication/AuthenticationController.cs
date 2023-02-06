using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Yumm_Back_End.Helper;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.SupportTeamDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Repository._Auth;
using Yumm_Back_End.Repository._Restaurant;
using Yumm_Back_End.Repository._SupportTeamRepository;
using Yumm_Back_End.Repository._UserRepository;
using Yumm_Back_End.Services;
using Yumm_Back_End.Services.Logger;

namespace Yumm_Back_End.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthRepository _authRepo;
        private readonly ILoggerService _loggerService;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        public IRestaurantRepository _restaurantRepo;
        private readonly IConfiguration _configuration;
        private readonly ISupportTeamRepository _supportTeamRepository;

        private const string BASEURL = "https://localhost:44464/";


        public AuthenticationController(IAuthRepository authRepo,
             UserManager<ApplicationUser> userManager, ILoggerService loggerService, IUserRepository userRepository, RoleManager<IdentityRole> roleManager,
              IRestaurantRepository restaurantRepo, IConfiguration configuration, ISupportTeamRepository supportTeamRepository
             )
        {
            _authRepo = authRepo;
            _userManager = userManager;
            _loggerService = loggerService;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _restaurantRepo = restaurantRepo;
            _configuration = configuration;
            _supportTeamRepository = supportTeamRepository;
        }


        #region Registration EndPoint
        [HttpPost]
        [Route("Restaurant/Register")]
        public async Task<IActionResult> RegisterRestaurant([FromBody] RegisterModelRestaurant model)
        {
            try
            {
                RoleIdentifier roleIdentifier = new RoleIdentifier();
                Generators gen = new Generators();



                if (model == null)
                {
                    return BadRequest();
                }


                if (!ModelState.IsValid)
                {
                    return StatusCode(500, ModelState);
                }


                //Distinguish the Role
                var role = roleIdentifier.ChooseRole(model.UserRole);

                if (role == null)
                {
                    ModelState.AddModelError("error", "failed to assign user role");

                    return BadRequest(ModelState);
                }



                //Preate a User object for the restaurant
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var code = _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (code == null)
                {
                    return BadRequest();
                }

                user.Token = code.Result;

                //Create user to the Database.
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }


                    return BadRequest(errorList);

                }
                           


                //Create the Specific Role and or associate a role to a user 
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }




                //Create the actual restaurant. 
                Restaurant restaurant = new Restaurant
                {
                    Name = model.Name,
                    Description = model.Description,
                    RestaurantTypeId = model.RestaurantTypeId,
                    LocationId = model.LocationId,
                    PhoneNumber = model.RestaurantPhoneNumber,
                    Email = model.RestaurantEmail,
                    Reference = gen.GenereateRestaurantReference(model.Name)
            };


                if (!await _restaurantRepo.CreateRestaurant(restaurant))
                {
                    ModelState.AddModelError("error", "error while trying to create restaurant.");
                    return BadRequest(ModelState);
                }


                //Prepare a user restaurant. 

                RestaurantUser restaurantUser = new RestaurantUser {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RestaurantId= restaurant.Id,
                    UserId = user.Id,
                };


                //Associate a user to restaurant
                if(! await _restaurantRepo.AssociateUserToRestaurant(restaurantUser))
                {

                    ModelState.AddModelError("error", "error while trying to associate user to restaurant.");
                    return BadRequest(ModelState);
                }



                EmailHelper emailHelper = new EmailHelper();

                var callBackUrl = BASEURL + "verifyuser?token=" + user.Token;

                //bool emailResponse = emailHelper.SendEmailMailTrapAsync(model.Email, callBackUrl);

                bool emailResponse = false;

                if (emailResponse)
                {
                    return StatusCode(StatusCodes.Status201Created,
                            new
                            {
                                Message = "Restaurant Create Successfully. Email Verification sent successfully.",
                                isSuccess = true
                            });
                }
                else
                {

                    return StatusCode(StatusCodes.Status201Created,
                            new
                            {
                                Message = "Restaurant Create Successfully. Email Verification not sent. Please request a new email verification. ",
                                isSuccess = true
                            });
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error occurred while attempting to create restaurant " + ex.Message);
                ModelState.AddModelError("error", "failed to create a restaurant.");
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [Route("Client/Register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientModel model)
        {
            try
            {


                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", $"Password and Password Confirmation do not match");

                    return StatusCode(500, ModelState);
                }


                if (!ModelState.IsValid)
                {
                    return StatusCode(500, ModelState);
                }

                var register = await _authRepo.RegisterClientAsync(model);

                return StatusCode(StatusCodes.Status201Created,
                            new
                            {
                                Message = "Client created successfully. Email Verification not sent. Please request a new email verification. ",
                                isSuccess = true
                            });

            }
            catch (Exception ex)
            {
                _loggerService.LogError("error " + ex.Message + " inner exception " + ex.InnerException.Message);
                ModelState.AddModelError("error", "Failed to process the request " + ex.Message);
                return BadRequest(ModelState);
            }
        }



       
        #endregion

        #region Verification EndPoints
        [HttpPost]
        [Route("Verify")]

        public async Task<IActionResult> VerifyRestaurantEmail(string token)
        {
            try
            {
                var user = _userRepository.GetVerifyUser(token);

                if (user == null)
                {
                    ModelState.AddModelError("error", "User could not be verified.");

                    return BadRequest();
                }


                user.EmailConfirmed = true;

                _userRepository.UpdateUser(user);


                return Ok(
                    new
                    {
                        isSuccess = true,
                        message = "Restaurant email verified successfully."
                    });
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error occurred while attempting to verify restaurant email " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("VerifyPhoneNumber")]
        public IActionResult VerifyRestaurantNumber(int OTP)
        {
            try
            {
                var user = _userRepository.GetUserToVerifyOTP(OTP);


                if (user == null)
                {
                    ModelState.AddModelError("error", "User could not be verified.");

                    return BadRequest();
                }



                user.PhoneNumberConfirmed = true;
                user.OTP = 0;
                _userRepository.UpdateUser(user);


                return Ok(
                    new
                    {
                        isSuccess = true,
                        message = "Restaurant email verified successfully."
                    });
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error occurred while attempting to verify restaurant email " + ex.Message);

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("RequestPhoneNumberVerification")]
        public IActionResult VerifyPhoneNumber(string phoneNumber)
        {
            try
            {
                TokenManager tokenManager = new TokenManager();


                var user = _userRepository.GetUserPhoneNumber(phoneNumber);

                if (user == null)
                {
                    ModelState.AddModelError("error", "User could not be verified.");

                    return BadRequest();
                }


                var generateOTP = tokenManager.GenerateOTP();

                user.OTP = generateOTP.OTP;

                _userRepository.UpdateUser(user);


                return Ok(
                    new
                    {
                        isSuccess = true,
                        message = "Restaurant email verified successfully."
                    });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion



        #region Authentication EndPoint
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginClient([FromBody] AuthModel authModel)
        {

            if (authModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var user = await _userManager.FindByEmailAsync(authModel.PhoneNumber);

            if (user == null)
            {
                var unauthorizedResponse = new { error = "Username or password is incorrect." };

                user = _authRepo.GetUserByUserName(authModel.PhoneNumber);

                if (user == null)
                {
                    //
                    if (authModel.PhoneNumber == null)
                    {
                        //ModelState.AddModelError("error", "Failed to authencticate this user");    
                        return Unauthorized(unauthorizedResponse);
                    }

                    var userByPhone = _authRepo.GetUserByPhone(authModel.PhoneNumber);

                    if (userByPhone == null)
                    {
                        return Unauthorized(unauthorizedResponse);
                    }

                    user = userByPhone;
                }

            }


            if (user != null && await _userManager.CheckPasswordAsync(user, authModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);


                var authClaims = new List<Claim>
                        {

                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]));

                var token = new JwtSecurityToken(
                    _configuration["AppSettings:Issuer"],
                    _configuration["AppSettings:Audience"],
                    claims: authClaims,
                     expires: DateTime.Now.AddDays(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                string fullName = "";


                if (user.Client != null)
                {
                    fullName = user.Client.FirstName + " " + user.Client.LastName;
                }
                //else if (user.Restaurant != null)
                //{

                //    fullName = user.Restaurant.Name;
                //}


                return Ok(new
                {
                    id = user.Id,
                    email = user.Email,
                    username = user.UserName,
                    fullName = fullName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    roles = userRoles,
                    expiration = token.ValidTo
                });
            }

            var authResult = await _authRepo.AuthenticatedClient(authModel.Password, authModel.UserName);

            return Ok(authResult);
        }

        #endregion

    }
}
