using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Mapper;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Repository._AddressRepository.ProvinceRepository;

namespace Yumm_Back_End.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceRepository _provinceRepo;
        private readonly IMapper _mapper;


        public ProvincesController(IProvinceRepository provinceRepo, IMapper mapper)
        {
            _mapper = mapper;
            _provinceRepo = provinceRepo;
        }

        [HttpGet]
        public IActionResult GetProvinces()
        {
            var provincesList = _provinceRepo.GetProvinces();

            var provincesDto = new List<ProvinceDTO>();


            foreach (var obj in provincesList)
            {
                provincesDto.Add(_mapper.Map<ProvinceDTO>(obj));
            }

            return Ok(provincesDto);
        }

        [HttpGet("{provinceId:int}", Name = "GetProvince")]

        public IActionResult GetProvince(int provinceId)
        {
            var provinceObj = _provinceRepo.GetProvince(provinceId);

            if (provinceObj == null)
            {
                return NotFound();
            }

            var provinceDto = _mapper.Map<ProvinceDTO>(provinceObj);

            return Ok(provinceDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvinceAsync([FromBody] ProvinceDTO provinceDTO)
        {
            if (provinceDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_provinceRepo.ProvinceExists(provinceDTO.Name))
            {
                ModelState.AddModelError("", "Provice already Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provinceObj = _mapper.Map<Province>(provinceDTO);

            if (!await _provinceRepo.CreateProvince(provinceObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {provinceObj.Name}");

                return StatusCode(500, ModelState);
            }

            //var province = _provinceRepo.CreateProvince();

            return Ok(provinceObj);

        }

        [HttpDelete]
        public IActionResult DeleteProvince(int provinceId)
        {
            if (!_provinceRepo.ProvinceExists(provinceId))
                {
                return NotFound();
                }

            var provinceObj = _provinceRepo.GetProvince(provinceId);

            if (!_provinceRepo.DeleteProvince(provinceObj))
                {
                ModelState.AddModelError("", $"Something went wrong while deleting the record{provinceObj.Name}");
                return StatusCode(500, ModelState);
                }

            return NoContent();
        }


        [HttpPatch("{provinceId:int}", Name = "UpdateProvinces")]
        public IActionResult UpdateProvinces(int provinceId,[FromBody] ProvinceDTO provinceDTO)
        { 
            if(provinceDTO ==null || provinceDTO.Id != provinceId)
            {
                return BadRequest(ModelState);
             }

            var provinceObj = _mapper.Map<Province>(provinceDTO);

            if (!_provinceRepo.UpdateProvice(provinceObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record{provinceObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
       
    }
}
