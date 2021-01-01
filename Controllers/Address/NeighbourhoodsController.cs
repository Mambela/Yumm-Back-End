using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository.NeighbourhoodRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighbourhoodsController : ControllerBase
    {

        private readonly INeighbourhoodRepository _hoodRepo;
        private readonly IMapper _mapper;

        public NeighbourhoodsController(INeighbourhoodRepository hoodRepo, IMapper mapper)
        {
            _hoodRepo = hoodRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNeighbourhood([FromBody] NeighbourhoodDTO hoodDTO)
        {
            if (hoodDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_hoodRepo.NeighbourhoodExists(hoodDTO.Name))
            {
                ModelState.AddModelError("", $"Neighbourhood already exists in the records!");
                return StatusCode(404, ModelState);
            }

            var hoodObj = _mapper.Map<Neighbourhood>(hoodDTO);

            if (!await _hoodRepo.CreateNeighbourhood(hoodObj))
            {
                ModelState.AddModelError("", $"Something went wrog when saving the record {hoodObj.Name}");

                return StatusCode(500, ModelState);
            }

            return Ok(hoodObj);
        }

        [HttpGet]
        public IActionResult GetNeighbourhoods()
        {
            var hoodList = _hoodRepo.GetNeighbourhoods();

            var hoodDto = new List<NeighbourhoodDTO>();

            foreach (var hood in hoodList)
            {
                hoodDto.Add(_mapper.Map<NeighbourhoodDTO>(hood));
            }

            return Ok(hoodDto);
        }

        [HttpGet("{hoodId:int}", Name ="GetNeighbourhood")]
        public IActionResult GetNeighbourhood(int hoodId) 
        {
            var hoodObj = _hoodRepo.GetNeighbourhood(hoodId);

            if (hoodObj == null)
            {
                return NotFound();
            }

            var hoodDto = _mapper.Map<NeighbourhoodDTO>(hoodObj);

            return Ok(hoodDto);
        }

        [HttpGet("GetNeighbourhoodInCommune/{communeId:int}")]
        public IActionResult GetNeighbourhoodInCommune(int communeId)
        {
            var hoodList = _hoodRepo.GetNeighbourhoodsInCommune(communeId);

            if (hoodList == null)
            {
                return NotFound();
            }

            var hoodDto = new List<NeighbourhoodDTO>();

            foreach (var hood in hoodList)
            {
                hoodDto.Add(_mapper.Map<NeighbourhoodDTO>(hood));
            }

            return Ok(hoodDto);
        }


        [HttpPatch]
        public IActionResult UpdateNeighbourhood([FromBody] NeighbourhoodDTO hoodDTO, int hoodId)
        {
            if (hoodDTO == null || hoodDTO.Id != hoodId)
            {
                return BadRequest(ModelState);
            }

            var hoodObj = _mapper.Map<Neighbourhood>(hoodDTO);

            if (!_hoodRepo.UpdateNeighbourhood(hoodObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {hoodObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteNeighbourhood(int hoodId)
        {
            if (!_hoodRepo.NeighbourhoodExists(hoodId))
            {
                return NotFound();
            }

            var hoodObj = _hoodRepo.GetNeighbourhood(hoodId);

            if (!_hoodRepo.DeleteNeighbourhood(hoodObj))
            {
                ModelState.AddModelError("", $"Something went wrong while trying to  update {hoodObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

    }
}
