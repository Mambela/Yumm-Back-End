using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository.CityRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepo;

        public CitiesController(ICityRepository cityRepo, IMapper mapper)
        {
            _mapper = mapper;
            _cityRepo = cityRepo;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var citiesList = _cityRepo.GetCities();

            var citiesDto = new List<CityDTO>();

            foreach (var city in citiesList)
            {
                citiesDto.Add(_mapper.Map<CityDTO>(city));
            }

            return Ok(citiesDto);
        }

        [HttpGet("{cityId:int}", Name = "GetCity")]
        public IActionResult GetCity(int cityId)
        {
            var cityObj = _cityRepo.GetCity(cityId);

            if (cityObj == null)
            {
                return NotFound();
            }

            var cityDto = _mapper.Map<CityDTO>(cityObj);

            return Ok(cityDto);
        }

        [HttpGet("GetCitiesInProvince/{provinceId:int}")]
        public IActionResult GetCitiesInProvince(int provinceId)
        {
            var citiesList = _cityRepo.GetCitiesInProvices(provinceId);

            if(citiesList == null)
            {
                return NotFound();
               }

            var cityDto = new List<CityDTO>();

            foreach (var city in citiesList)
            {
                cityDto.Add(_mapper.Map<CityDTO>(city));
            }


            return Ok(cityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityDTO cityDto)
        {
            if (cityDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_cityRepo.CityExists(cityDto.Name))
            {
                ModelState.AddModelError("", "City already Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityObj = _mapper.Map<City>(cityDto);

            if (!await _cityRepo.CreateCity(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record{cityObj.Name}");

                return StatusCode(500, ModelState);
            }



            return Ok(cityObj);
        }


        [HttpPatch]
        public IActionResult UpdateCity([FromBody] CityDTO cityDto, int cityId)
        {
            if (cityDto == null || cityDto.Id != cityId)
            {
                return BadRequest(ModelState);
            }

            var cityObj = _mapper.Map<City>(cityDto);

            if (!_cityRepo.UpdateCity(cityObj))
                {
                ModelState.AddModelError("", $"Something went wrog while updating the record {cityObj.Name}");
                return StatusCode(500, ModelState);
                }

            return NoContent();

        }

        [HttpDelete]
        public IActionResult DeleteCity(int cityId)
        {
            if (!_cityRepo.CityExists(cityId))
            {
                return NotFound();
            }

            var cityObj = _cityRepo.GetCity(cityId);

            if (!_cityRepo.DeleteCity(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong while trying to  update {cityObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }
        
    }
}
