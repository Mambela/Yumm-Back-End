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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantRepository restRepo, IMapper mapper)
        {
            _restaurantRepo = restRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] _RestaurantDTO restaurantDTO)
        {
            if (restaurantDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_restaurantRepo.RestaurantExist(restaurantDTO.Name))
            {
                ModelState.AddModelError("", $"The restaurant already exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurantObj = _mapper.Map<Restaurant>(restaurantDTO);

            if (!await _restaurantRepo.CreateRestaurant(restaurantObj))
            {
                ModelState.AddModelError("", $"An error occurred while creating the restaurant {restaurantDTO.Name}");

                return StatusCode(500, ModelState);
            }

            return Ok(restaurantObj);

        }

        [HttpGet]
        public IActionResult GetRestaurants()
        {
            var restaurantList = _restaurantRepo.GetRestaurants();

            var restaurantDTO = new List<_RestaurantDTO>();

            foreach (var restaurant in restaurantList)
            {
                restaurantDTO.Add(_mapper.Map<_RestaurantDTO>(restaurant));
            }

            return Ok(restaurantDTO);
        }


        [HttpGet("{restaurantId:int}", Name ="GetRestaurant")]
        public IActionResult GetRestaurant(int restaurantId)
        {
            var restaurantObj = _restaurantRepo.GetRestaurant(restaurantId);

            if (restaurantObj == null)
            {
                return NotFound();
            }

            var restaurantDTO = _mapper.Map<_RestaurantDTO>(restaurantObj);

            return Ok(restaurantDTO);

        }

        [HttpGet("GetRestaurantByType/{typeId:int}")]
        public IActionResult GetRestaurantByType(int typeId)
        {
            var restaurantList = _restaurantRepo.GetRestaurantsInType(typeId);

            var restaurantDTO = new List<_RestaurantDTO>();

            foreach (var restaurant in restaurantList)
            {
                restaurantDTO.Add(_mapper.Map<_RestaurantDTO>(restaurant));
            }

            return Ok(restaurantDTO);

        }

        [HttpDelete]
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepo.RestaurantExist(restaurantId))
            {
                return NotFound();
            }

            var restaurantObj = _restaurantRepo.GetRestaurant(restaurantId);

            if (!_restaurantRepo.DeleteRestaurant(restaurantObj))
                {
                    ModelState.AddModelError("", $"Something went wrong while deleting the record{restaurantObj.Name}");
                    return StatusCode(500, ModelState);
                }

            return NoContent();
        }

        [HttpPatch]

        public IActionResult UpdateRestaurant([FromBody] _RestaurantDTO restaurantDTO, int restaurantId)
        {
            if (restaurantDTO == null || restaurantDTO.Id == restaurantId)
            {
                return BadRequest(ModelState);
            }

            var restaurantObj = _mapper.Map<Restaurant>(restaurantDTO);

            if(!_restaurantRepo.UpdateResturant(restaurantObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the restaurant {restaurantObj.Name}");

                return StatusCode(500, ModelState);
             }

            return NoContent();
        }

    }
}
