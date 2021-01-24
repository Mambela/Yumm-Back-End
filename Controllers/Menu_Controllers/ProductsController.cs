using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._MenuRepository;
using Yumm_Back_End.Repository._MenuRepository._ProductRepository;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private MediaResponse uploadResponse;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepo = productRepository;
            _mapper = mapper;
            uploadResponse = new MediaResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodProduct([FromForm] CreateProductDTO productDto)
        {


            if (productDto == null)
            {
                return BadRequest(ModelState);
            }

            if (productDto.Files == null)
            {
                return BadRequest(ModelState);
            }



            uploadResponse = await _productRepo.SaveProductImage(productDto);

            if (!uploadResponse.Success)
            {
                ModelState.AddModelError("", $" " + uploadResponse.Message);
                return StatusCode(500, ModelState);
            }


            productDto.ImagePath = uploadResponse.Message;



            if (_productRepo.ProductExists(productDto.Name, productDto.RestaurantId, productDto.CategoryId))
            {
                ModelState.AddModelError("", $"Product that you are trying to add already Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objProduct = _mapper.Map<Product>(productDto);

            if (!await _productRepo.CreateProduct(objProduct))
            {
                ModelState.AddModelError("", $"Something went wrong when Saving the record {objProduct.Name}");
            }


            return Ok(objProduct);


        }

        [HttpGet("{productId:int}", Name = "GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            var productObj = _productRepo.GetProduct(productId);

            if (productObj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ProductDTO>(productObj);

            return Ok(objDto);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = _productRepo.GetProducts();

            var productDto = new List<ProductDTO>();

            foreach (var product in productList)
            {
                productDto.Add(_mapper.Map<ProductDTO>(product));
            }

            return Ok(productDto);
        }

        [HttpGet("GetProductsInCategory/{categoryId:int}")]
        public IActionResult GetProductsInCategory(int categoryId)
        {
            var productList = _productRepo.GetProductsInCategory(categoryId);

            var productDto = new List<ProductDTO>();

            foreach (var product in productList)
            {
                productDto.Add(_mapper.Map<ProductDTO>(product));
            }

            return Ok(productDto);

        }


        [HttpGet("GetProductsInMenuGroup/{menuGroupId:int}")]
        public IActionResult GetProductsInMenuGroup(int menuGroupId)
        {
            var productList = _productRepo.GetProductsInMenuGroup(menuGroupId);

            var productDto = new List<ProductDTO>();

            foreach (var product in productList)
            {
                productDto.Add(_mapper.Map<ProductDTO>(product));
            }

            return Ok(productDto);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductAsync([FromForm] UpdateProductDTO productDto, int productId)
        {

            if (productDto == null || productDto.Id != productId)
            {
                return BadRequest(ModelState);
            }

            if (productDto.Files == null)
            {
                return BadRequest(ModelState);
            }


            var objForImage = _mapper.Map<CreateProductDTO>(productDto);


            uploadResponse = await _productRepo.SaveProductImage(objForImage);

            if (!uploadResponse.Success)
            {
                ModelState.AddModelError("", $" " + uploadResponse.Message);
                return StatusCode(500, ModelState);
            }


            productDto.ImagePath = uploadResponse.Message;



            var productObj = _mapper.Map<Product>(productDto);

            if (!_productRepo.UpdateProduct(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {productObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {


            if (!_productRepo.ProductExists(productId))
            {
                return NotFound();
            }


            var objProduct = _productRepo.GetProduct(productId);
            if (!_productRepo.DeleteProduct(objProduct))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {objProduct.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();

        }

    }
}
