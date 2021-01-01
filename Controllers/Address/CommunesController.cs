using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository.CommuneRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunesController : ControllerBase
    {
        private readonly ICommuneRepository _IComRepo;
        private readonly IMapper _mapper;

        public CommunesController(ICommuneRepository communeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _IComRepo = communeRepo;
        }


        [HttpPost]

        public async Task <IActionResult> CreateCommune([FromBody] CommuneDTO communeDTO)
        {
            if (communeDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_IComRepo.CommuneExists(communeDTO.Name))
            {
                ModelState.AddModelError("", $"Commune already exists in the records!");
                return StatusCode(404, ModelState);
            }

            var communeObj = _mapper.Map<Commune>(communeDTO);

            if (!await _IComRepo.CreateCommune(communeObj))
            {
                ModelState.AddModelError("", $"Something went wrog when saving the record {communeObj.Name}");

                return StatusCode(500, ModelState);
            }

            return Ok(communeObj);


        }


        [HttpGet]
        public IActionResult GetCommunes()
        {
            var communeList = _IComRepo.GetCommunes();

            var communeDto = new List<CommuneDTO>();

            foreach (var commune in communeList)
            {
                communeDto.Add(_mapper.Map<CommuneDTO>(commune));
            }


            return Ok(communeDto);

        }


        [HttpGet("{communeId:int}", Name = "GetCommune")]
        public IActionResult GetCommune(int communeId)
        {
            var communeObj = _IComRepo.GetCommune(communeId);

            if (communeObj == null)
            {
                return NotFound();
            }

            var communeDto = _mapper.Map<CommuneDTO>(communeObj);

            return Ok(communeDto);
        }

        [HttpGet("GetCommunesInCity/{cityId:int}")]
        public IActionResult GetCommunesInCity(int cityId)
        {
            var communeList = _IComRepo.GetCommunesInCity(cityId);

            if (communeList == null)
            {
                return NotFound();
            }

            var communeDto = new List<CommuneDTO>();

            foreach (var commune in communeList)
            {
                communeDto.Add(_mapper.Map<CommuneDTO>(commune));
            }


            return Ok(communeDto);
        }


        [HttpPatch]

        public IActionResult UpdateCity([FromBody] CommuneDTO communeDTO, int communeId)
        {
            if (communeDTO == null || communeDTO.Id != communeId)
            {
                return BadRequest(ModelState);
            }

            var communeObj = _mapper.Map<Commune>(communeDTO);

            if (!_IComRepo.UpdateCommune(communeObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {communeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        
        [HttpDelete]
        public IActionResult DeleteCommune(int communeId)
        {
            if (!_IComRepo.CommuneExists(communeId))
            {
                return NotFound();
            }

            var cityObj = _IComRepo.GetCommune(communeId);

            if (!_IComRepo.DeleteCommune(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong while trying to  update {cityObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }



    }
}
