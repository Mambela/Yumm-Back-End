using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._ProductCategory;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoRepo;
        private readonly IMapper _mapper;

        public FoodCategoriesController(ICategoryRepository categoRepo, IMapper mapper)
        {
            _categoRepo = categoRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                ModelState.AddModelError("", $"Cannot Add an empty error");
                return BadRequest(ModelState);
            }

            if (_categoRepo.CategoryExists(categoryDTO.Name))
            {
                ModelState.AddModelError("", "Sorry But the Category already exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDTO);

            if (!await _categoRepo.CreateCategory(categoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {categoryObj.Name}");
                return StatusCode(500, ModelState);
            }


            return Ok(categoryObj);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {

            var categoryList = _categoRepo.GetCategories();

            var categoryDTO = new List<CategoryDTO>();

            foreach (var category in categoryList)
            {
                categoryDTO.Add(_mapper.Map<CategoryDTO>(category));
            }

            return Ok(categoryDTO);
        }

        [HttpGet("{categoryId:int}", Name ="GetCategory")]
        public IActionResult GetCategory(int categoryId)
        {
            var categoryObj = _categoRepo.GetCategory(categoryId);

            if (categoryObj == null)
            {
                return NotFound();
            }

            var categoryDTO = _mapper.Map<Category>(categoryObj);

            return Ok(categoryDTO);

        }

        [HttpPatch]
        public IActionResult UpdateCategory([FromBody] CategoryDTO categoryDTO, int CategoryId)
        {
            if (categoryDTO == null || categoryDTO.Id != CategoryId)
            {
                return BadRequest(ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDTO);

            if (!_categoRepo.UpdateCategory(categoryObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {categoryObj.Name}");
                    return StatusCode(500, ModelState);
                }

            return NoContent();

        }

        [HttpDelete]
        public IActionResult DeleteCategory(int categoryId)
        { 
            if(!_categoRepo.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryObj = _categoRepo.GetCategory(categoryId);

            if (!_categoRepo.DeleteCategory(categoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {categoryObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
