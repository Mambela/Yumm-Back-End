using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Helper;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._Restaurant;
using Yumm_Back_End.Services;
using Yumm_Back_End.Services.MediaService;
using Yumm_Back_End.Services.PaginationService;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Controllers.Restaurant_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IMediaManager mediaManager;
        public RestaurantsController(IRestaurantRepository restRepo, IMapper mapper, IUriService uriService, IMediaManager _mediaManager)
        {
            _restaurantRepo = restRepo;
            _mapper = mapper;
            _uriService = uriService;
            this.mediaManager = _mediaManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] _RestaurantDTO restaurantDTO)
        {
            Generators gen = new Generators();

            try
            {

                if (restaurantDTO == null)
                {
                    return BadRequest(ModelState);
                }
                                 
           
                if (_restaurantRepo.RestaurantExist(restaurantDTO.Name))
                {
                    ModelState.AddModelError("", $"The restaurant already exists!");
                    return StatusCode(404, ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var restaurantObj = _mapper.Map<Restaurant>(restaurantDTO);
                    restaurantObj.Reference = gen.GenereateRestaurantReference(restaurantDTO.Name);

                if (!await _restaurantRepo.CreateRestaurant(restaurantObj))
                {
                    ModelState.AddModelError("", $"An error occurred while creating the restaurant {restaurantDTO.Name}");

                    return StatusCode(500, ModelState);
                }

            return Ok(restaurantObj);

            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpGet]

        public IActionResult GetPagedRestaurant([FromQuery] PaginationFilter filters)
        {
            try
            {
                TokenManager token = new TokenManager();
                var route = Request.Path.Value;

                var validFilter = new PaginationFilter(filters.PageNumber,filters.PageSize);

                var pagedDataObj = _restaurantRepo.GetRestaurantsPaged(validFilter);

                var pageDataDTO = new List<FullRestaurantDTO>();

                foreach (var item in pagedDataObj)
                {

                    pageDataDTO.Add(
                        new FullRestaurantDTO 
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Email = item.Email,
                            Name = item.Name,
                            NoOfOrders = token.GenerateOrders(),
                            Phone = item.PhoneNumber,
                            RestaurantType = item.RestaurantType.Name,
                               Reference = item.Reference,
                               Logo = item.Logo,
                            CreateDate = item.CreateDate,
                            UpdatedDate = item.UpdatedDate
                        }
                        );

                }

                var totalRecords = pagedDataObj.Count();

                var pagedResponse = PaginationHelper.CreatePageResponse<FullRestaurantDTO>(pageDataDTO, validFilter, totalRecords, _uriService, route);

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet("PagedRestaurant/reference:string")]
        public IActionResult GetPagedRestaurantByRef([FromQuery] PaginationFilter filters, string reference)
        {
            try
            {
                TokenManager token = new TokenManager();
                var route = Request.Path.Value;

                var validFilter = new PaginationFilter(filters.PageNumber, filters.PageSize);

                var pagedDataObj = _restaurantRepo.GetRestaurantsByReference(validFilter,reference);

                var pageDataDTO = new List<FullRestaurantDTO>();

                foreach (var item in pagedDataObj)
                {

                    pageDataDTO.Add(
                        new FullRestaurantDTO
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Email = item.Email,
                            Name = item.Name,
                            NoOfOrders = token.GenerateOrders(),
                            Phone = item.PhoneNumber,
                            RestaurantType = item.RestaurantType.Name,
                            Reference = item.Reference,
                            Logo = item.Logo,
                            CreateDate = item.CreateDate,
                            UpdatedDate = item.UpdatedDate
                        }
                        );

                }

                var totalRecords = pagedDataObj.Count();

                var pagedResponse = PaginationHelper.CreatePageResponse<FullRestaurantDTO>(pageDataDTO, validFilter, totalRecords, _uriService, route);

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{restaurantId:int}", Name ="GetRestaurant")]
        public IActionResult GetRestaurant(int restaurantId)
        {
            var restaurantObj = _restaurantRepo.GetRestaurant(restaurantId);

            if (restaurantObj == null)
            {
                return NotFound();
            }

            var restaurantDTO = _mapper.Map<ViewRestauratDTO>(restaurantObj);

            restaurantDTO.RestaurantType = restaurantObj.RestaurantType.Name;

            return Ok(restaurantDTO);

        }

        [HttpGet("GetRestaurantByType/{typeId:int}")]
        public IActionResult GetRestaurantByType(int typeId)
        {
            var restaurantList = _restaurantRepo.GetRestaurantsInType(typeId);

            var restaurantDTO = new List<_RestaurantDTO>();

            foreach (var restaurant in restaurantList)
            {
                restaurantDTO.Add(_mapper.Map<_RestaurantDTO>(restaurant));
            }

            return Ok(restaurantDTO);

        }

        [HttpDelete]
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepo.RestaurantExist(restaurantId))
            {
                return NotFound();
            }

            var restaurantObj = _restaurantRepo.GetRestaurant(restaurantId);

            if (!_restaurantRepo.DeleteRestaurant(restaurantObj))
                {
                    ModelState.AddModelError("", $"Something went wrong while deleting the record{restaurantObj.Name}");
                    return StatusCode(500, ModelState);
                }

            return NoContent();
        }

        [HttpPatch]

        public async Task<IActionResult> UpdateRestaurantAsync([FromForm] _RestaurantDTO restaurantDTO, int restaurantId)
        {
            try
            {

                    if (restaurantDTO == null || restaurantDTO.Id != restaurantId)
                    {
                                return BadRequest(ModelState);
                    }

                    MediaResponse uploadedResponse = new MediaResponse();

                    if (restaurantDTO.Files != null)
                    {
                        string images = "\\images\\";
                        string entity = "\\" + "restaurant" + "\\";
                        string reference = "\\" + restaurantId.ToString() + "\\";
                        uploadedResponse = await mediaManager.SaveImage(restaurantDTO.Files, reference, entity, images);
                    }


                    var restaurantObj = _restaurantRepo.GetRestaurant(restaurantId);

                    if (restaurantObj == null)
                    {
                        ModelState.AddModelError("error", "Request restaurant not found.");

                        return NotFound(ModelState);
                    }


                    restaurantObj.PhoneNumber = restaurantDTO.PhoneNumber;
                    restaurantObj.Email = restaurantDTO.Email;
                    restaurantObj.Name = restaurantDTO.Name;
                    restaurantObj.Description = restaurantDTO.Description;
                    restaurantObj.RestaurantTypeId = restaurantDTO.RestaurantTypeId;
                
                    restaurantObj.LocationId = restaurantDTO.LocationId;


                    if (uploadedResponse.isSucess)
                    {
                            restaurantObj.Logo = uploadedResponse.imagePath;
                    }



                    if (!_restaurantRepo.Save())
                    {
                        ModelState.AddModelError("", $"Something went wrong while updating the restaurant {restaurantObj.Name}");

                        return StatusCode(500, ModelState);
                     }



                    return NoContent();

            }
            catch (Exception ex)
            {


                throw;
            }
        }

    }
}
