using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._MenuRepository._MenuGroupRepository;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuGroupsController : ControllerBase
    {
        private IMenuGroupRepository _menuGroupRepo;
        private IMapper _mapper;

        public MenuGroupsController(IMenuGroupRepository menuGroupRepo, IMapper mapper)
        {
            _menuGroupRepo = menuGroupRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuGroupAsync([FromBody] CreateMenuGroupDTO menuGroupDTO)
        {
            if (menuGroupDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var menuGroup = _mapper.Map<MenuGroup>(menuGroupDTO);

            if (!await _menuGroupRepo.CreateRestaurantMenuGroup(menuGroup))
            {
                ModelState.AddModelError("", $"Error occurred while creating Menu group. Please try again later");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok(menuGroup);
        }


        [HttpGet]
        public IActionResult GetMenuGroups()
        {
            var menuGroupList = _menuGroupRepo.GetMenuGroups();

            var menuGroupObj = new List<MenuGroupDTO>();

            foreach (var menugroup in menuGroupList)
            {
                menuGroupObj.Add(_mapper.Map<MenuGroupDTO>(menugroup));
            }

            return Ok(menuGroupObj);
        }

        [HttpGet("{menuGroupId:int}",Name = "GetMenuGroup")]
        public IActionResult GetMenuGroup(int menuGroupId)
        {
            //if(!_menuGroupRepo.CheckMenuGroupExist(menuGroupId))
            //  {
            //    return NotFound();
            //    }

            var menuGroupObj = _menuGroupRepo.GetRestaurantMenuGroup(menuGroupId);

            if (menuGroupObj == null)
            {
                return NotFound();
            }

            var menuGroupDTO = _mapper.Map<MenuGroupDTO>(menuGroupObj);


            return Ok(menuGroupDTO);


        }

        [HttpGet("GetMenuGroupsInRestaurant/{restaurantId:int}")]
        public IActionResult GetMenuGroupsInRestaurant(int restaurantId)
        {
            var menuGroupList = _menuGroupRepo.GetRestaurantMenuGroups(restaurantId);

          
            var menuGroupInRestaurantList = new List<MenuGroupDTO>();


            foreach (var item in menuGroupList)
            {
                var menugroupItem = new MenuGroupDTO
                {
                    Id = item.Id,
                    Name = item.Name,

                };

                menuGroupInRestaurantList.Add(menugroupItem);
            }

            return Ok(menuGroupInRestaurantList);
        }

        [HttpDelete]
        public IActionResult DeleteMenuGroup(int menugroupId)
        {
            if (_menuGroupRepo.CheckMenuGroupExist(menugroupId))
               {
                return NotFound();
                }

            var menuGroupDTO = _menuGroupRepo.GetRestaurantMenuGroup(menugroupId);

            var menuGroup = _mapper.Map<MenuGroup>(menuGroupDTO);

            if (!_menuGroupRepo.DeleteMenu(menuGroup))
               {
                    ModelState.AddModelError("", $"Error occurred while trying to update the Restaurant Menu Group");
                    return StatusCode(500, ModelState);
                }


            return NoContent();

        }


        [HttpPatch]
        public IActionResult UpdateMenuGroup(CreateMenuDTO menuGroupDTO, int menugroupId)
        {
            if (menuGroupDTO == null || menuGroupDTO.Id != menugroupId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            var menuGroup = _mapper.Map<MenuGroup>(menuGroupDTO);

            if (!_menuGroupRepo.UpdateMenu(menuGroup))
            {
                ModelState.AddModelError("", $"Error occurred while trying to update Menu Group");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
