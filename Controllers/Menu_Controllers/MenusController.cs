using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._MenuRepository;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {

        private IMenuRepository _menuRepo;
        private IMapper _mapper;
        public MenusController(IMenuRepository menuRepo, IMapper mapper)
        {
            _menuRepo = menuRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuAsync(CreateMenuDTO menuDTO)
        {
            if (menuDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var menuObj = _mapper.Map<Menu>(menuDTO);


            if (!await _menuRepo.CreateRestaurantMenu(menuObj))
            {

                ModelState.AddModelError("", $"There was a error while creating a menu ");
                return StatusCode(500, menuDTO);
            }



            return Ok(menuObj);

        }

        [HttpGet]
        public IActionResult GetMenus()
        {
            var menuList = _menuRepo.GetMenus();

            if (menuList == null)
            {
                return NotFound();
            }

            //var menuObj = new List<Menu>();
            var menuObj = new List<MenuDTO>();

            foreach (var menu in menuList)
            {

                //menuObj.Add(_mapper.Map<Menu>(menu));
                var menuDTO = new MenuDTO
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    RestaurantId = menu.Restaurant.Id,
                    RestaurantName = menu.Restaurant.Name
                };

                menuObj.Add(menuDTO);

            }

            return Ok(menuObj);
        }


        [HttpGet("{menuId:int}", Name = "GetMenu")]
        public IActionResult GetMenu(int menuId)
        {
            var menuObj = _menuRepo.GetMenu(menuId);

            if (menuObj == null)
            {
                return NotFound();
            }

            //var menuDTO = _mapper.Map<CreateMenuDTO>(menuObj);

            var menuDTO = new MenuDTO
            {
                Id = menuObj.Id,
                Name = menuObj.Name,
                RestaurantId = menuObj.RestaurantId,
                RestaurantName = menuObj.Restaurant.Name
            };

            return Ok(menuDTO);
        }


        //[HttpGet("{restaunrantId:int}", Name = "GetMenusInRestaurant")]
        [HttpGet("GetMenusInRestaurant/{restaunrantId:int}")]
        public IActionResult GetMenusInRestaurant(int restaunrantId)
        {
            var menuList = _menuRepo.GetRestaurantMenus(restaunrantId);

            if (menuList == null)
            {
                return NotFound();
            }

            var menuObj = new List<MenuDTO>();

            foreach (var menu in menuList)
            {
                //menuObj.Add(_mapper.Map<Menu>(menu));
                var menuDTO = new MenuDTO
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    RestaurantId = menu.Restaurant.Id,
                    RestaurantName = menu.Restaurant.Name
                };

                menuObj.Add(menuDTO);
            }

            return Ok(menuObj);
        }

        [HttpDelete]
        public IActionResult DeleteMenu(int menuId)
        {
            if (_menuRepo.MenuExists(menuId))
            {
                return NotFound();
            }

            var menuObj = _menuRepo.GetMenu(menuId);

            if(!_menuRepo.DeleteMenu(menuObj))
            {
                ModelState.AddModelError("", "Error occurred while trying to delete the Menu");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }


        [HttpPatch]
        public IActionResult UpdateMenu(CreateMenuDTO MenuUpdateDTO, int menuID)
        {
                if (MenuUpdateDTO == null || MenuUpdateDTO.Id != menuID)
                {
                    return BadRequest();
                    }

                if(!ModelState.IsValid)
                {
                    return StatusCode(500, ModelState);
                }


                if (_menuRepo.MenuExists(menuID))
                {
                    ModelState.AddModelError("", "Error occurred, the Menu you have just request doesn't exist");
                    return StatusCode(404, ModelState);
                }

                var menuObj = _mapper.Map<Menu>(MenuUpdateDTO);

                if (!_menuRepo.UpdateMenu(menuObj))
                {
                    ModelState.AddModelError("", "Error occured while trying to update the Menu");
                    return StatusCode(404, ModelState);
                }


            return NoContent();

            }

    }
}
