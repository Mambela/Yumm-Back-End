using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Helper;
using Yumm_Back_End.Models.Authentication;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.SupportTeamDTO;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Repository._UserRepository;
using Yumm_Back_End.Repository._SupportTeamRepository;
using Yumm_Back_End.Services.PaginationService;
using Yumm_Back_End.Models.DTOs.DriversDTO;

namespace Yumm_Back_End.Controllers.SupportTeam_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTeamController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        private readonly IUriService _uriService;
        private readonly ISupportTeamRepository _supportTeamRepository;

        private const string BASEURL = "https://localhost:44464/";


        public SupportTeamController(
            ILoggerService loggerService, RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository, ISupportTeamRepository supportTeamRepository,
            UserManager<ApplicationUser> userManager,
            IUriService uriService)
        {
            _loggerService = loggerService;
            _userManager = userManager;
            _roleManager = roleManager;
            _supportTeamRepository = supportTeamRepository;
            _uriService = uriService;
        }


        [HttpGet]
        public IActionResult GetSupportTeam([FromQuery] PaginationFilter filter)
        {
            try
            {
                var route = Request.Path.Value;
                var supportTeamObjList = _supportTeamRepository.GetSupportTeams(filter);

                var pagedSupportTeam = new List<ViewSupportTeamDTO>();

                foreach (var supportTeam in supportTeamObjList)
                {

                    pagedSupportTeam.Add(
                        new ViewSupportTeamDTO
                        {
                            Id = supportTeam.Id,
                            Email = supportTeam.User.Email,
                            Name = supportTeam.Name,
                            LastName = supportTeam.LastName,
                            Gender = supportTeam.Gender,
                            IdNumber = supportTeam.IdNumber,
                            PhoneNumber = supportTeam.User.PhoneNumber
                        }
                       );

                }

                var pagedResponse = PaginationHelper.CreatePageResponse<ViewSupportTeamDTO>(pagedSupportTeam, filter, supportTeamObjList.Count, _uriService, route);

                return Ok(pagedResponse);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Error occured while trying to retrieve support team.");
                return BadRequest(ModelState);
            }
        }


        [HttpGet("RetrieveSupportTeam/supportTeamId:int")]
        public IActionResult RetrieveSupportTeam(int supportTeamId)
        {
            try
            {
                var supportTeamObj = _supportTeamRepository.GetSupportTeam(supportTeamId);


                var viewSupportTeam = new ViewSupportTeamDTO
                {
                    Id = supportTeamObj.Id,
                    Email = supportTeamObj.User.Email,
                    Gender = supportTeamObj.Gender,
                    IdNumber = supportTeamObj.IdNumber,
                    Name = supportTeamObj.Name,
                    PhoneNumber = supportTeamObj.User.PhoneNumber,
                    LastName = supportTeamObj.LastName,
                    
                };




                return Ok(viewSupportTeam);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Error occured while trying to retrieve support team.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("Register/Supportteam")]
        public async Task<IActionResult> CreateSupportTeamMember([FromBody] SupportTeamDTO supportTeamDTO)
        {
            try
            {

                if (supportTeamDTO == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (supportTeamDTO.Password != supportTeamDTO.ConfirmPassword)
                {
                    ModelState.AddModelError("errors", "User password and confirmation do not match");
                    return BadRequest(ModelState);
                }

                ApplicationUser user = new ApplicationUser
                {
                    Email = supportTeamDTO.Email,
                    UserName = supportTeamDTO.Username,
                    PhoneNumber = supportTeamDTO.PhoneNumber,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };


                var code = _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (code == null)
                {
                    return BadRequest();
                }


                user.Token = code.Result;


                var result = await _userManager.CreateAsync(user, supportTeamDTO.Password);

                if (!result.Succeeded)
                {
                    var errorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }

                    return BadRequest(errorList);
                }


                if (!await _roleManager.RoleExistsAsync(UserRoles.SUPPORTTEAM))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.SUPPORTTEAM));

                if (await _roleManager.RoleExistsAsync(UserRoles.SUPPORTTEAM))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.SUPPORTTEAM);
                }


                SupportTeam supportTeam = new SupportTeam
                {
                    Name = supportTeamDTO.Name,
                    LastName = supportTeamDTO.LastName,
                    Gender = supportTeamDTO.Gender,
                    IdNumber = supportTeamDTO.IdNumber,
                    UserId = user.Id
                };

                if (!await _supportTeamRepository.CreateSupportTeam(supportTeam))
                {

                    ModelState.AddModelError("error", "error while trying to create support team");
                    return BadRequest(ModelState);
                }


                EmailHelper emailHelper = new EmailHelper();

                var callBackUrl = BASEURL + "verifyuser?token=" + user.Token;

                bool emailResponse = emailHelper.SendEmailMailTrapAsync(supportTeamDTO.Email, callBackUrl);

                if (emailResponse)
                {
                    return StatusCode(StatusCodes.Status201Created,
                            new
                            {
                                Message = "User created Successfully. Email Verification sent successfully.",
                                isSuccess = true
                            });
                }
                else
                {

                    return StatusCode(StatusCodes.Status201Created,
                            new
                            {
                                Message = "User created Successfully. Email Verification not sent. Please request a new email verification. ",
                                isSuccess = true
                            });
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError("error " + ex.Message + " inner exception " + ex.InnerException.Message);
                ModelState.AddModelError("error", "Failed to process the request " + ex.Message);
                return BadRequest(ModelState);
            }
        }









    }
}
