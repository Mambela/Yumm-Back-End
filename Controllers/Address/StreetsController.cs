using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository.StreetRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly IStreetRepository _streetRepo;
        private readonly IMapper _mapper;

        public StreetsController(IMapper mapper,IStreetRepository streetRepo)
        {
            _mapper = mapper;
            _streetRepo = streetRepo;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStreet([FromBody] StreetDTO streetDTO)
        {
            if (streetDTO == null)
            {
                return BadRequest(ModelState);
            }


            if (_streetRepo.StreetExists(streetDTO.Name))
            {
                ModelState.AddModelError("", $"Neighbourhood already exists in the records!");
                return StatusCode(404, ModelState);
            }

            var streetObj = _mapper.Map<Street>(streetDTO);

            if (!await _streetRepo.CreateStreet(streetObj))
                {
                    ModelState.AddModelError("", $"Something went wrog when saving the record {streetObj.Name}");

                    return StatusCode(500, ModelState);
                }


            return Ok(streetObj);
        }


        [HttpGet]
        public IActionResult GetStreets()
        {
            var streetList = _streetRepo.GetStreets();

            var streetDto = new List<StreetDTO>();

            foreach (var street in streetList)
            {

                streetDto.Add(_mapper.Map<StreetDTO>(street));
            }

            return Ok(streetDto);
        }

        [HttpGet("{streetId:int}", Name ="GetStreet")]
        public IActionResult GetStreet(int streetId)
        {
            var streetObj = _streetRepo.GetStreet(streetId);

            if(streetObj ==null)
            {
                return NotFound();
            }

            var streetDto = _mapper.Map<StreetDTO>(streetObj);

            return Ok(streetDto);
        }


        [HttpGet("GetStreetsInNeighbourhood/{hoodId:int}")]
        public IActionResult GetStreetsInNeighbourhood(int hoodId)
        {
            var streetList = _streetRepo.GetStreetsInNeighbourhoods(hoodId);

            if (streetList == null)
            {
                return NotFound();
            }

            var streetDto = new List<StreetDTO>();

            foreach (var street in streetList)
            {
                streetDto.Add(_mapper.Map<StreetDTO>(street));
            }

            return Ok(streetDto);
        }

        [HttpPatch]
        public IActionResult UpdateStreet([FromBody] StreetDTO streetDTO, int streetId)
        {
            if (streetDTO == null || streetDTO.Id != streetId)
            {
                return BadRequest(ModelState);
            }

            var streetObj = _mapper.Map<Street>(streetDTO);

            if (!_streetRepo.UpdateStreet(streetObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {streetObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteStreet(int streetId)
        {
            if (!_streetRepo.StreetExists(streetId))
            {
                return NotFound();
            }

            var cityObj = _streetRepo.GetStreet(streetId);

            if (!_streetRepo.DeleteStreet(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong while trying to  update {cityObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

    }
}
