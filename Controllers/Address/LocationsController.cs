using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Repository._AddressRepository.LocationRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly ILocationRepository _locRepo;

        //public LocationsController(ILocationRepository locRepo)
        //{
        //    _locRepo = locRepo;
        //}

        //[HttpPost]
        //public async Task <IActionResult> CreateLocation([FromBody] Location location)
        //{

        //    if (location == null)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    if (_locRepo.LocationExists(location))
        //    {
        //        ModelState.AddModelError("", $"Location already exists in the records!");
        //        return StatusCode(404, ModelState);
        //    }

         

        //    if (!await _locRepo.CreateLocation(location))
        //    {
        //        ModelState.AddModelError("", $"Something went wrog when saving the Location in {location.StreetName}");

        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok(location);
        //}


        //[HttpGet]
        //public IActionResult GetLocations()
        //{
        //    var locationList = _locRepo.GetLocations();

        //    return Ok(locationList);
        //}


        //[HttpGet("{locationId:int}", Name ="GetLocation")]
        //public IActionResult GetLocation(int locationId)
        //{
        //    var locationsList = _locRepo.GetLocation(locationId);

        //    if (locationsList == null)
        //    {
        //        ModelState.AddModelError("", $"Location doesn't exist in the records!");
        //        return StatusCode(404, ModelState);
                
        //    }

        //    return Ok(locationsList);
        //}

        //[HttpGet("GetLocationInNeighbourhood/{hoodName}")]
        //public IActionResult GetLocationInNeighbourhood(string hoodName)
        //{
        //    var locationList = _locRepo.GetLocationsInNeighbourhood(hoodName);

        //    if (locationList == null)
        //       {
        //        ModelState.AddModelError("", $"Neighbourhood doesn't exist in the records!");
        //        return StatusCode(404, ModelState);
        //    }

        //    return Ok(locationList);
        //}

        //[HttpPatch]
        //public IActionResult Updatelocation([FromBody] Location location, int locationId)
        //{
        //    if (location == null || location.Id != locationId)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!_locRepo.UpdateLocation(location))
        //    {
        //        ModelState.AddModelError("", $"Something went wrong while updating the record with the street {location.StreetName}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}

        //[HttpDelete]

        //public IActionResult DeleteLocation(int locationId)
        //{
        //    if (!_locRepo.LocationExists(locationId))
        //    {
        //        return NotFound();
        //    }


        //    if (!_locRepo.DeleteLocation(locationId))
        //    {
        //        ModelState.AddModelError("", $"Something went wrong while trying to  update the record with the street ");
        //        return StatusCode(500, ModelState);
        //    }


        //    return NoContent();
        //}
    }
}
