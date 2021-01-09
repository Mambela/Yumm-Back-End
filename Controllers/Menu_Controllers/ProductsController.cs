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
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _itemRepository;
        private readonly IMapper _mapper;
        private MediaResponse uploadResponse;

        public ProductsController(IProductRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            uploadResponse = new MediaResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodItem([FromForm] CreateProductDTO itemDto)
        {


            if (itemDto == null)
            {
                return BadRequest(ModelState);
            }

            if (itemDto.Files == null)
            {
                return BadRequest(ModelState);
            }

            

            uploadResponse = await _itemRepository.SaveProductImage(itemDto);

            if (!uploadResponse.Success)
                {
                ModelState.AddModelError("", $" "+uploadResponse.Message);
                return StatusCode(500, ModelState);
                }

            
            itemDto.ImagePath = uploadResponse.Message;
           


            if (_itemRepository.ItemExists(itemDto.ItemName, itemDto.RestaurantId, itemDto.CategoryId))
            {
                ModelState.AddModelError("", $"Item that you are trying to add already Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objItem = _mapper.Map<Product>(itemDto);

            if (!await _itemRepository.CreateItem(objItem))
            {
                ModelState.AddModelError("", $"Something went wrong when Saving the record {objItem.Name}");
            }


            return Ok(objItem);


        }

        [HttpGet("{itemId:int}", Name = "GetItem")]
        public IActionResult GetItem(int itemId)
        {
            var itemObj = _itemRepository.GetItem(itemId);

            if (itemObj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ProductDTO>(itemObj);

            return Ok(objDto);
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var itemList = _itemRepository.GetItems();

            var itemDto = new List<ProductDTO>();

            foreach (var item in itemList)
            {
                itemDto.Add(_mapper.Map<ProductDTO>(item));
            }

            return Ok(itemDto);
        }

        [HttpGet("GetItemsInCategory/{categoryId:int}")]
        public IActionResult GetItemsInCategory(int categoryId)
        {
            var itemList = _itemRepository.GetItemsInCategory(categoryId);

            var itemDto = new List<ProductDTO>();

            foreach (var item in itemList)
            {
                itemDto.Add(_mapper.Map<ProductDTO>(item));
            }

            return Ok(itemDto);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateItemAsync([FromForm] UpdateProductDTO itemDto, int itemId)
        { 

            if(itemDto == null || itemDto.Id != itemId)
            {
                return BadRequest(ModelState);
              }

            if (itemDto.Files == null)
            {
                return BadRequest(ModelState);
            }


            var objForImage = _mapper.Map<CreateProductDTO>(itemDto);


            uploadResponse = await _itemRepository.SaveProductImage(objForImage);

            if (!uploadResponse.Success)
            {
                ModelState.AddModelError("", $" " + uploadResponse.Message);
                return StatusCode(500, ModelState);
            }


            itemDto.ImagePath = uploadResponse.Message;



            var itemObj = _mapper.Map<Product>(itemDto);

            if (!_itemRepository.UpdateItem(itemObj))
                {
                ModelState.AddModelError("", $"Something went wrong when updating the record {itemObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

            }

        [HttpDelete]
        public IActionResult DeleteItem(int itemId)
        {
            

            if (!_itemRepository.ItemExists(itemId))
            {
                return NotFound();
            }


            var objItem = _itemRepository.GetItem(itemId);
            if (!_itemRepository.DeleteItem(objItem))
                {
                    ModelState.AddModelError("", $"Something went wrong when deleting the record {objItem.Name}");
                    return StatusCode(500, ModelState);
                }


            return NoContent();

        }

    }
}
