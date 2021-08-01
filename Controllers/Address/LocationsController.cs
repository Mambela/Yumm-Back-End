using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Models.DTOs.AddressDTO.Location;
using Yumm_Back_End.Repository._AddressRepository.LocationRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly ILocationRepository _locRepo;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository locRepo, IMapper mapper)
        {
            _mapper = mapper;
            _locRepo = locRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDTO locationDTO)
        {

            if (locationDTO == null)
            {
                return BadRequest(ModelState);
            }

            var location = _mapper.Map<Location>(locationDTO);


            if (_locRepo.LocationExists(location))
            {
                ModelState.AddModelError("", $"Location already exists in the records!");
                return StatusCode(404, ModelState);
            }



            if (!await _locRepo.CreateLocation(location))
            {

                return StatusCode(500, ModelState);
            }

            return Ok(location);
        }


        [HttpGet]
        public IActionResult GetLocations()
        {
            var locationList = _locRepo.GetLocations();


            var locationDTOList = new List<LocationDTO>();


            foreach (var location in locationList)
            {
                LocationDTO locationObj = new LocationDTO
                {
                    Id = location.Id,
                    Street = location.Street.Name,
                    Lat = location.Lat,
                    Lng = location.Lng
                };

                locationDTOList.Add(locationObj);
            }

            return Ok(locationDTOList);
        }


        [HttpGet("{locationId:int}", Name = "GetLocation")]
        public IActionResult GetLocation(int locationId)
        {
            var locationObj = _locRepo.GetLocation(locationId);

            if (locationObj == null)
            {
                ModelState.AddModelError("", $"Location doesn't exist in the records!");
                return StatusCode(404, ModelState);

            }


            LocationDTO location = new LocationDTO
            {
                Id = locationObj.Id,
                Street = locationObj.Street.Name,
                Lat = locationObj.Lat,
                Lng = locationObj.Lng
            };


            return Ok(location);
        }

        [HttpGet("GetLocationInNeighbourhood/{hoodId:int}")]
        public IActionResult GetLocationInNeighbourhood(int hoodId)
        {
            var locationList = _locRepo.GetLocationsInNeighbourhood(hoodId);

            if (locationList == null)
            {
                ModelState.AddModelError("", $"Neighbourhood doesn't exist in the records!");
                return StatusCode(404, ModelState);
            }


            var locationDTOList = new List<LocationDTO>();


            foreach (var location in locationList)
            {
                LocationDTO locationObj = new LocationDTO
                {
                    Id = location.Id,
                    Street = location.Street.Name,
                    Lat = location.Lat,
                    Lng = location.Lng
                };

                locationDTOList.Add(locationObj);

            }

            return Ok(locationDTOList);
        }

        [HttpPatch]
        public IActionResult Updatelocation([FromBody] LocationCreateDTO locationDTO, int locationId)
        {
            if (locationDTO == null || locationDTO.Id != locationId)
            {
                return BadRequest(ModelState);
            }

            var location = _mapper.Map<Location>(locationDTO);

            if (!_locRepo.UpdateLocation(location))
            {
                //ModelState.AddModelError("", $"Something went wrong while updating the record with the street {location.StreetName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]

        public IActionResult DeleteLocation(int locationId)
        {
            if (!_locRepo.LocationExists(locationId))
            {
                return NotFound();
            }


            if (!_locRepo.DeleteLocation(locationId))
            {
                ModelState.AddModelError("", $"Something went wrong while trying to  update the record with the street ");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }
    }
}
