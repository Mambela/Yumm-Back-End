using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._Restaurant;

namespace Yumm_Back_End.Controllers.Restaurant_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsTypeController : ControllerBase
    {
        public readonly IRestaurantTypeRepository _restTypeRepo;
        public readonly IMapper _mapper;

        public RestaurantsTypeController(IRestaurantTypeRepository restTypeRepo, IMapper mapper)
        {
            _restTypeRepo = restTypeRepo;
            _mapper = mapper;
        }

        [HttpPost]

        public async Task<IActionResult> CreateType([FromBody] RestaurantTypeDTO restaurantType)
        {
            if (restaurantType == null)
            {
                return BadRequest(ModelState);
            }

            if (_restTypeRepo.TypeExists(restaurantType.Name))
            {

                ModelState.AddModelError("error", "Type already Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeObj = _mapper.Map<RestaurantType>(restaurantType);
            if (!await _restTypeRepo.CreateType(typeObj))
            {
                ModelState.AddModelError("error", $"An error occurred while creating the type {restaurantType.Name}");

                return StatusCode(500, ModelState);
            }

            return Ok(typeObj);
        }

        [HttpGet("{typeId:int}", Name = "GetType")]
        public IActionResult GetType(int typeId)
        {
            var typeObj = _restTypeRepo.GetRestaurantType(typeId);

            if (typeObj == null)
            {
                return NotFound();
            }

            var typeDTO = _mapper.Map<RestaurantTypeDTO>(typeObj);

            return Ok(typeDTO);
        }


        [HttpGet("GetRestaurantsByType/{typeId:int}")]
        public IActionResult GetRestaurantInType(int typeId)
        {
            var typeList = _restTypeRepo.GetRestaurantsInTypes(typeId);


            var typeDTO = new List<RestaurantTypeDTO>();

            foreach (var restType in typeList)
            {
                typeDTO.Add(_mapper.Map<RestaurantTypeDTO>(restType));
            }

            return Ok(typeDTO);
        }

        [HttpGet]
        public IActionResult GetTypes()
        {
            var typeList = _restTypeRepo.GetRestaurantTypes();


            var typeDTO = new List<RestaurantTypeDTO>();

            foreach (var restType in typeList)
            {
                typeDTO.Add(_mapper.Map<RestaurantTypeDTO>(restType));
            }

            return Ok(typeDTO);

        }

        [HttpPatch]
        public IActionResult UpdateType([FromBody] RestaurantTypeDTO typeDTO, int typeId)
        {
            if (typeDTO == null || typeDTO.Id != typeId)
            {
                return BadRequest(ModelState);
            }

            var typeObj = _mapper.Map<RestaurantType>(typeDTO);

            if (!_restTypeRepo.UpdateType(typeObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record{typeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteType(int typeId)
        {

            if (!_restTypeRepo.TypeExists(typeId))
            {
                return NotFound();
            }

            var typeObj = _restTypeRepo.GetRestaurantType(typeId);

            if (!_restTypeRepo.DeleteType(typeId))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting the record{typeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
