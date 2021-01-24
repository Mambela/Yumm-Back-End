using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs.AddressDTO.Location;
using Yumm_Back_End.Repository._AddressRepository.LocationRepository;
using Yumm_Back_End.Repository._AddressRepository.UserLocationRepository;
using Yumm_Back_End.Repository._UserRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersLocationsController : ControllerBase
    {
        private IUserLocationRepository _userLocRepo;
        private ILocationRepository _locationRepo;
        private IMapper _mapper;
        private IUserRepository _userRepo;


        public UsersLocationsController(IUserLocationRepository userLocationRepository, ILocationRepository locationRepo, IMapper mapper, IUserRepository userRepo)
        {
            _userLocRepo = userLocationRepository;
            _locationRepo = locationRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserLocationAsync([FromBody] CreateUserLocationDTO _createUserLocation)
        {


            if (_createUserLocation == null)
            {
                return BadRequest();
            }

            // Here there are two ways to saving the user's location.


            //A user needs to provide a location. Even if its not their current location but they need to find the closest location possible.


            //var checkedLocation = _locationRepo(locationObj);

            if (!_locationRepo.LocationExists(_createUserLocation.LocationId))
            {
                ModelState.AddModelError("", $"Location doesn't exists in the records!");
                return StatusCode(404, ModelState);
            }


            var locationObj = _locationRepo.GetLocation(_createUserLocation.LocationId);

            var user = _userRepo.GetUser(_createUserLocation.UserId);

            var userlocation = new UsersLocations
            {
                User = user,
                Location = locationObj,
                Name = _createUserLocation.Name,
                Unit = _createUserLocation.Unit,
                Description = _createUserLocation.Description,
                _StreetSetByUser = _createUserLocation._StreetSetByUser,
                Lat = _createUserLocation.Lat,
                Lng = _createUserLocation.Lng
            };


            if (!await _userLocRepo.CreateUserLocations(userlocation))
            {
                ModelState.AddModelError("", $"An error occured while trying to save the user location");
                return StatusCode(404, ModelState);
            }


            return StatusCode(201, userlocation);
        }


        [HttpGet(Name = "GetAUserLocations")]
        public IActionResult GetAUserLocations(string userId)
        {

            var listOfUserLoca = _userLocRepo.GetAllUserLocations(userId);

            if (listOfUserLoca == null)
            {
                ModelState.AddModelError("", $"The user doesn't have any registered Locations");
                return StatusCode(404, ModelState);
            }


            var userlocationObj = new List<UserLocationDTO>();

            if (userlocationObj == null)
            {
                ModelState.AddModelError("", $"Error while getting the location from the user");
                return StatusCode(500, ModelState);
            }


            foreach (var userlocation in listOfUserLoca)
            {
                var location = _locationRepo.GetLocation(userlocation.LocationId);

                UserLocationDTO userLocationDTO = new UserLocationDTO
                {
                    UserId = userlocation.UserId,
                    Name = userlocation.Name,
                    Unit = userlocation.Unit,
                    Description = userlocation.Description,

                    LocationId = location.Id,
                    City = location.City.Name,
                    Neighbourhood = location.Neighbourhood.Name,
                    Commune = location.Commune.Name,
                    Street = location.Street.Name,
                    Lat = userlocation.Lat,
                    Lng = userlocation.Lng
                };

                userlocationObj.Add(userLocationDTO);
            }


            return Ok(userlocationObj);

        }


        [HttpGet("GetAUserLocation/{userId}/{locationId:int}")]
        public IActionResult GetAUserLocation(string userId, int locationId)
        {

            var userLocation = _userLocRepo.GetUserLocation(userId, locationId);

            var location = _locationRepo.GetLocation(userLocation.LocationId);

            UserLocationDTO userLocationDTO = new UserLocationDTO
            {
                UserId = userLocation.UserId,
                Name = userLocation.Name,
                Unit = userLocation.Unit,
                Description = userLocation.Description,

                LocationId = location.Id,
                City = location.City.Name,
                Neighbourhood = location.Neighbourhood.Name,
                Commune = location.Commune.Name,
                Street = location.Street.Name,
                Lat = userLocation.Lat,
                Lng = userLocation.Lng
            };


            return Ok(userLocationDTO);
        }


        [HttpDelete]
        public IActionResult DeleteUserLocation(string userId, int locationId)
        {
            var userLocation = _userLocRepo.GetUserLocation(userId, locationId);

            if (userLocation == null)
            {
                ModelState.AddModelError("", $"Error occurred while processing the user location, Please check that both a locaiton is provided");
                return StatusCode(404, ModelState);
            }


            if (!_userLocRepo.DeleteUserLocation(userLocation))
            {
                ModelState.AddModelError("", $"Error occurred! User's location not deleted");
                return StatusCode(500, ModelState);
            }

            return StatusCode(204);
        }


        [HttpPatch]
        public IActionResult UpdateUserLocation([FromBody] CreateUserLocationDTO userLocationDTO, int locationId)
        {
            if (userLocationDTO == null || userLocationDTO.LocationId != locationId)
            {
                return BadRequest(ModelState);
            }

            var userLocationObj = _mapper.Map<UsersLocations>(userLocationDTO);

            if (!_userLocRepo.UpdateUserLocation(userLocationObj))
            {
                ModelState.AddModelError("", $"An error occurred while updating the User's Location");
                return StatusCode(500, ModelState);
            }


            return StatusCode(204);
        }
    
    }
}
