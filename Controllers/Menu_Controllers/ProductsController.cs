using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._MenuRepository;
using Yumm_Back_End.Repository._MenuRepository._MenuGroupRepository;
using Yumm_Back_End.Repository._MenuRepository._ProductRepository;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Services.PaginationService;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IMenuGroupRepository _menuGroupRepo;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private MediaResponse uploadResponse;
        private readonly IUriService _uriService;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IMenuGroupRepository menuGroup, ILoggerService loggerService, IUriService uriService)
        {
            _loggerService = loggerService;
            _productRepo = productRepository;
            _mapper = mapper;
            _menuGroupRepo = menuGroup;
            uploadResponse = new MediaResponse();
            _uriService = uriService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodProduct([FromForm] CreateProductDTO productDto)
        {

            try
            {
                if (productDto == null)
                {
                    return BadRequest(productDto);
                }

                if (productDto.Files == null)
                {
                    return BadRequest(ModelState);
                }

               string _entity = "\\" + "restaurant" + "\\";
                

                uploadResponse = await _productRepo.SaveProductImage(productDto,productDto.RestaurantId,_entity);

                if (!uploadResponse.isSucess)
                {
                    ModelState.AddModelError("", $"failed to upload product Image.");
                    return StatusCode(500, ModelState);
                }


                productDto.ImagePath = uploadResponse.imagePath;



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

                    return BadRequest(ModelState);
                }


                var productSizeList = new List<ProductSize>();

                //Associate product size to the  Product
                foreach (var productProperty in productDto.Sizes)
                {
                    productSizeList.Add(new ProductSize
                    {
                        ProductId = objProduct.Id,
                        Size = productProperty.Size,
                        Price = productProperty.Price,

                    });
                }


                if (!await _productRepo.CreateProductSize(productSizeList))
                {
                    ModelState.AddModelError("", $"Error occurred while trying to save product price and size");
                    return BadRequest(ModelState);
                }


                //Associate Product To MenuGroup
                var menugroupProduct = new MenuGroupsProducts
                {
                    MenuGroupId = productDto.MenuGroupId,
                    ProductId = objProduct.Id
                };




                if (!await _menuGroupRepo.AttachProductToMenugroup(menugroupProduct))
                {
                    ModelState.AddModelError("", $"Unable to attach product to menu group");

                    return BadRequest(ModelState);
                }


                productDto.Id = objProduct.Id;

                return StatusCode(StatusCodes.Status201Created, productDto);

            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error " + ex.Message);

                ModelState.AddModelError("error", "Failed to complete product add process. Try again.");

                return BadRequest(ModelState);
            }


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
        public IActionResult GetProducts([FromQuery] PaginationFilter pagination)
        {
            try
            {

                var productList = _productRepo.GetProducts(pagination);
                var route = Request.Path.Value;
                var productDto = new List<ViewProductDTO>();

                foreach (var product in productList)
                {

                    var productItem = new ViewProductDTO
                    {
                        Id = product.Id,
                        Category = product.category.Name,
                        Name = product.Name,
                        Description = product.Description,
                        Duration = product.Duration,
                        ImagePath = product.ImagePath,
                        CategoryId = product.categoryId,
                        MenuGroup = product.MenuGroup.Name,
                        ProductSizes = _mapper.Map<IList<ProductSizesDTO>>(product.ProductSizes),
                    };
                    productDto.Add(productItem);
                }

                int totalRecords = _productRepo.CountAllProducts();

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewProductDTO>(productDto, validFilter, totalRecords, _uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Failed to complete product retrieval process. Try again.");

                return BadRequest(ModelState);

            }
        }

        [HttpGet("ProductsInCategory/{categoryId:int}")]
        public IActionResult ProductsInCategory(int categoryId, [FromQuery] PaginationFilter pagination)
        {
            try
            {
                

                var productList = _productRepo.GetProductsInCategory(categoryId);
                var route = Request.Path.Value;
                var productDto = new List<ProductDTO>();

                foreach (var product in productList)
                {
                    productDto.Add(_mapper.Map<ProductDTO>(product));
                }

                int totalRecords = _productRepo.CountProdcutsInCategory(categoryId);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ProductDTO>(productDto, validFilter, totalRecords, _uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Failed to complete product retrieval process. Try again.");

                return BadRequest(ModelState);
            }
        }


        [HttpGet("RestaurantsProductsInCategory")]
        public IActionResult GetRestaurantsProductsInCategory(int categoryId, int restaurantId,[FromQuery] PaginationFilter pagination)
        {
            try {

                var productList = _productRepo.GetProductsInaRestaurantbyCategory(pagination, restaurantId,categoryId);
                var route = Request.Path.Value;
                var productDto = new List<ViewProductDTO>();

                foreach (var product in productList)
                {

                    var productItem = new ViewProductDTO
                    {
                        Id = product.Id,
                        Category = product.category.Name,
                        Name = product.Name,
                        Description = product.Description,
                        Duration = product.Duration,
                        ImagePath = product.ImagePath,
                        CategoryId = product.categoryId,
                        MenuGroup = product.MenuGroup.Name,
                        ProductSizes = _mapper.Map<IList<ProductSizesDTO>>(product.ProductSizes),
                    };
                    productDto.Add(productItem);
                }

                int totalRecords = _productRepo.CountProductsInaRestaurantbyCategory(restaurantId,categoryId);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewProductDTO>(productDto, validFilter, totalRecords, _uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Failed to complete product retrieval process. Try again.");

                return BadRequest(ModelState);
            }
        }
        


        [HttpGet("ProductsInRestauarant/restauarantId:int")]
        public IActionResult ProductsInRestauarant(int restauarantId, [FromQuery] PaginationFilter pagination)
        {
            try
            {

                var productList = _productRepo.GetProductsInRestaurant(pagination, restauarantId);
                var route = Request.Path.Value;
                var productDto = new List<ViewProductDTO>();

                foreach (var product in productList)
                {

                    var productItem = new ViewProductDTO
                    {
                        Id = product.Id,
                        Category = product.category.Name,
                        Name = product.Name,
                        Description = product.Description,
                        Duration = product.Duration,
                        ImagePath = product.ImagePath,
                        CategoryId = product.categoryId,
                        MenuGroup = product.MenuGroup.Name,
                        ProductSizes = _mapper.Map<IList<ProductSizesDTO>>(product.ProductSizes),
                    };
                    productDto.Add(productItem);
                }

                int totalRecords = _productRepo.CountProductsInRestaurant(restauarantId);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewProductDTO>(productDto, validFilter, totalRecords, _uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Failed to complete product retrieval process. Try again.");

                return BadRequest(ModelState);

            }
        }

        [HttpGet("GetProductsInMenuGroup/{menuGroupId:int}")]
        public IActionResult GetProductsInMenuGroup(int menuGroupId, [FromQuery] PaginationFilter pagination)
        {
            try
            {


                var productList = _productRepo.GetProductsInMenuGroup(pagination, menuGroupId);
                var route = Request.Path.Value;
                var productDto = new List<ProductDTO>();

                foreach (var product in productList)
                {
                    productDto.Add(_mapper.Map<ProductDTO>(product));
                }
                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                int totalRecords = _productRepo.CountProductsInMenuGroup(menuGroupId);

                var pageResponse = PaginationHelper.CreatePageResponse<ProductDTO>(productDto, validFilter, totalRecords, _uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error " + ex.Message);
                ModelState.AddModelError("error", "Failed to complete product retrieval process. Try again.");

                return BadRequest(ModelState);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductAsync([FromForm] UpdateProductDTO productDto, int productId)
        {

            if (productDto == null || productDto.Id != productId)
            {
                return BadRequest(ModelState);
            }

            var productObj = _productRepo.GetProduct(productId);

            if (productDto.Files != null)
            {
                var objForImage = _mapper.Map<CreateProductDTO>(productDto);
                 
                string entity = "\\" + "restaurant" + "\\";
                uploadResponse = await _productRepo.SaveProductImage(objForImage,productDto.RestaurantId,entity);
                productObj.ImagePath = uploadResponse.imagePath;    
            }

            productObj.Duration = productDto.Duration;
            productObj.MenuGroupId = productDto.MenuGroupId;
            productObj.categoryId = productDto.CategoryId;
            productObj.Description = productDto.Description;


            if (!_productRepo.UpdateProduct(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {productObj.Name}");
                return StatusCode(500, ModelState);
            }


            //Delete current product. 
            var currentProductSize = _productRepo.GetAllProductSizes(productObj.Id);

            if (_productRepo.RemoveProductSize(currentProductSize))
            {
                var productSizeList = new List<ProductSize>();

                //Associate product size to the  Product
                foreach (var productProperty in productDto.Sizes)
                {
                    productSizeList.Add(new ProductSize
                    {
                        ProductId = productObj.Id,
                        Size = productProperty.Size,
                        Price = productProperty.Price,

                    });
                }


                if (! await _productRepo.CreateProductSize(productSizeList))
                {
                    return Ok(
                        new 
                            {
                             isSuccess = false,
                             message= "Failed to update product size. Please try again."
                            });

                }
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
