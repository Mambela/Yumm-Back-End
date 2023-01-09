using AutoMapper;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Yumm_Back_End.Helper;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Drivers;
using Yumm_Back_End.Models.DTOs.DriversDTO;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Repository._DriverRepository;
using Yumm_Back_End.Repository._UserRepository;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Services.MediaService;
using Yumm_Back_End.Services.PaginationService;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Controllers.Drivers_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository driverRepo;
        private readonly ILoggerService loggerService;
        private readonly IUserRepository _userRepo;
        private readonly MediaResponse uploadedResponse;
        public RoleManager<IdentityRole> _roleManager;
        private readonly IMediaManager mediaManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public DriversController(IDriverRepository driverRepository, ILoggerService _loggerService, IMediaManager _mediaManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IUriService uriService,
            IUserRepository userrepo)
        {
            driverRepo = driverRepository;
            loggerService = _loggerService;
            uploadedResponse = new MediaResponse();
            mediaManager = _mediaManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _uriService = uriService;
            _userRepo = userrepo;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDriver([FromForm] CreateDriverDTO driverDTO)
        {
            try
            {

                if (driverDTO == null)
                {
                    return BadRequest();
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (driverDTO.Files == null)
                {
                    ModelState.AddModelError("error", "Please provided a profile picture for the driver");
                    return BadRequest(ModelState);
                }



                if (driverDTO.Password != driverDTO.Confirmation_Password)
                {

                    ModelState.AddModelError("error", "Password and password confirmation do not match");
                    return BadRequest(ModelState);
                }

                string images = "\\images\\";
                string entity = "\\" + "driver" + "\\";
                string reference = "\\" + "profilepicture" + "\\";

                var uploadedResponse = await mediaManager.SaveImage(driverDTO.Files,reference, entity, images);

                if (!uploadedResponse.Success)
                {
                    ModelState.AddModelError("", $" " + uploadedResponse.imagePath);
                    return StatusCode(500, ModelState);
                }

                driverDTO.ImageLink = uploadedResponse.imagePath;
                TokenManager token = new TokenManager();


                ApplicationUser user = new ApplicationUser {
                    Email = driverDTO.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = driverDTO.Username,
                    PhoneNumber = driverDTO.PhoneNumber
                };

                user.OTP = token.GenerateOTP().OTP;

                //var code =_userManager.GenerateEmailConfirmationTokenAsync(user);
                //return StatusCode(StatusCodes.Status201Created);

                var result = await _userManager.CreateAsync(user, driverDTO.Password);

                if (!result.Succeeded)
                {
                    var errorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }

                    return BadRequest(errorList);
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.DRIVER))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.DRIVER));

                if (await _roleManager.RoleExistsAsync(UserRoles.DRIVER))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.DRIVER);
                }

                Driver driver = new Driver {
                    FirstName = driverDTO.FirstName,
                    LastName = driverDTO.LastName,
                    Gender = driverDTO.Gender,
                    ImageLink = uploadedResponse.imagePath,
                    UserId = user.Id,
                    AddressId = driverDTO.AddressId,

                };


                if (!await driverRepo.CreateDriver(driver))
                {
                    ModelState.AddModelError("error", "Error occured while trying to create a new driver ");

                    return BadRequest(ModelState);
                }



                return StatusCode(StatusCodes.Status201Created,
                     new
                     {
                         Message = "Driver Create Successfully. Please verify driver account.",
                         isSuccess = true
                     });
            }
            catch (Exception ex)
            {

                loggerService.LogError("Error " + ex.Message);

                ModelState.AddModelError("error", "Error occured while trying to create a new driver");

                return BadRequest(ModelState);
            }
        }

        [HttpGet("{driverId:int}",Name = "GetDriversList")]
        public IActionResult GetDriver(int driverId)
        {
            try
            {

                var driverObj = driverRepo.GetDriver(driverId);

                var driverDTO = new ViewDriverDTO
                {
                    Id = driverObj.Id,
                    FirstName = driverObj.FirstName,
                    LastName = driverObj.LastName,
                    Email = driverObj.User.Email,
                    Gender = driverObj.Gender,
                    PhoneNumber = driverObj.User.PhoneNumber,
                    ImageLink = driverObj.ImageLink,
                    CreateDate = driverObj.CreateDate,
                    AddressId = driverObj.AddressId,

                };


                return Ok(driverDTO);
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error " + ex.Message);

                ModelState.AddModelError("error", "Error occured while trying to retrieve driver details.");

                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public IActionResult GetDriversList([FromQuery] PaginationFilter filter)
        {
            try
            {
                var route = Request.Path.Value;

                var pagedDriverListObj = driverRepo.GetDriversList(filter);


                var pagedDriverDTO = new List<ViewDriverDTO>();


                foreach (var driverItem in pagedDriverListObj) 
                {

                    pagedDriverDTO.Add(
                        new ViewDriverDTO
                        {
                            Id = driverItem.Id,
                            FirstName = driverItem.FirstName,
                            LastName = driverItem.LastName,
                            Email = driverItem.User.Email,
                            Gender = driverItem.Gender,
                            Blocked = driverItem.User.Blocked,
                            PhoneNumber = driverItem.User.PhoneNumber,
                            ImageLink = driverItem.ImageLink,
                            CreateDate = driverItem.CreateDate,
                            AddressId   =driverItem.AddressId,  

                        }
                        );
                }


                int totalRecords = driverRepo.TotalRecords();
                var pagedResponse = PaginationHelper.CreatePageResponse<ViewDriverDTO>(pagedDriverDTO, filter, totalRecords, _uriService, route);

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Error occured while trying to retrieve driver details.");
                return BadRequest(ModelState);
            }
        }


        [HttpPatch]
        public async Task<IActionResult> UpdateDriverDetailsAsync([FromForm] CreateDriverDTO driverDTO, [FromQuery] int driverid)
        {
            try
            {
                if (driverDTO == null || driverDTO.Id != driverid)
                  {
                    return BadRequest(ModelState);
                }

                MediaResponse uploadedResponse = new MediaResponse();

                if (driverDTO.Files != null) 
                {
                    string images = "\\images\\";
                    string entity = "\\" + "driver" + "\\";
                    string reference = "\\" + "profilepicture" + "\\";
                    uploadedResponse = await mediaManager.SaveImage(driverDTO.Files, reference, entity, images);
                }


                var driverCurrentObj = driverRepo.GetDriver(driverid);

                if (driverCurrentObj == null)
                {
                    ModelState.AddModelError("error", "Request driver not found.");

                    return NotFound(ModelState);
                }


                driverCurrentObj.FirstName = driverDTO.FirstName;
                driverCurrentObj.LastName = driverDTO.LastName;
                driverCurrentObj.Gender = driverDTO.Gender;
                driverCurrentObj.AddressId = (driverDTO.AddressId == 0)? driverCurrentObj.AddressId:driverDTO.AddressId;



                driverCurrentObj.ImageLink = (uploadedResponse.Success) ? uploadedResponse.imagePath:driverCurrentObj.ImageLink;

                //if (!driverRepo.UpdateDriver(driverObj))
                if (!driverRepo.Save())
                    {
                    ModelState.AddModelError("", $"Something went wrong while updating the restaurant {driverCurrentObj.FirstName} ");

                    return StatusCode(500, ModelState);
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Error occured while trying to update driver details.");
                return BadRequest(ModelState);
            }
        }
    }
}
