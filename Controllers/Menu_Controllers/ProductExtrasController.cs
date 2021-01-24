using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._MenuRepository._ProductRepository;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductExtrasController : ControllerBase
    {
        private IProductExtrasRepository _productExtrasRepo;

        private IMapper _mapper;

        public ProductExtrasController(IProductExtrasRepository productExtrasRepo,IMapper mapper)
        {
            _mapper = mapper;
            _productExtrasRepo = productExtrasRepo;

        }


        [HttpPost]
        public async Task<IActionResult> CreateProductExtrasAsync([FromBody]CreateProductExtrasDTO creProExtraDTO)
        {
            if (creProExtraDTO == null)
            {

                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var producExtrDTO = _mapper.Map<ProductExtras>(creProExtraDTO);


            if (!await _productExtrasRepo.CreateProductExtras(producExtrDTO))
            {
                ModelState.AddModelError("", $"An error occurred while attemting to save a new Product Extra");
                return StatusCode(500, ModelState);
            }


            return Ok(producExtrDTO);

        }


        [HttpGet("{productExtraId:int}", Name = "GetProductExtra")]

        public IActionResult GetProductExtra(int productExtraId)
        {
            var productExtra = _productExtrasRepo.GetProductExtra(productExtraId);

            if (productExtra == null)
            {
                return NotFound();
            }

            var productExtraDTO = _mapper.Map<CreateProductExtrasDTO>(productExtra);


            return Ok(productExtraDTO);
        }


        [HttpGet]
        public IActionResult GetProductExtras()
        {
            var productExtraList = _productExtrasRepo.GetProductExtras();


            var productExtrasDTOList = new List<CreateProductExtrasDTO>();

            foreach (var prodExtra in productExtraList)
            {
                productExtrasDTOList.Add(_mapper.Map<CreateProductExtrasDTO>(prodExtra));
            }

            return Ok(productExtrasDTOList);

        }



        [HttpGet("GetAllProductExtrasInProduct/{productExtraId:int}")]
        public IActionResult GetAllProductExtrasInProduct(int productExtraId)
        {
            var productExtraList = _productExtrasRepo.GetProductExtras(productExtraId);


            var productExtrasDTOList = new List<CreateProductExtrasDTO>();

            foreach (var prodExtra in productExtraList)
            {
                productExtrasDTOList.Add(_mapper.Map<CreateProductExtrasDTO>(prodExtra));
            }

            return Ok(productExtrasDTOList);

        }

        [HttpDelete]
        public IActionResult DeleteProductExtra( int productExtraId)
        {
           


              if(_productExtrasRepo.ProductExtrasExist(productExtraId))
                {
                    ModelState.AddModelError("", $"Product extra you are trying to update could not be found!");
                    return StatusCode(500, ModelState);
                }

            var productExtra = _productExtrasRepo.GetProductExtra(productExtraId);


            if (!_productExtrasRepo.DeleteProductExtra(productExtra))
            {
                ModelState.AddModelError("", $"An error occurred while trying to update the Products Extra");

                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateProductExtra([FromBody] CreateProductExtrasDTO productExtraDTO,int productExtraId)
        {
            if (productExtraDTO == null || productExtraDTO.Id != productExtraId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var productExtraObj = _mapper.Map<ProductExtras>(productExtraDTO);


            if (!_productExtrasRepo.UpdateProductExtra(productExtraObj))
            {
                ModelState.AddModelError("", $"Error occurred while deleting the product {productExtraObj.Name} please try again later");
                return StatusCode(500, ModelState);
            }


            return NoContent();


        }
    }
}
