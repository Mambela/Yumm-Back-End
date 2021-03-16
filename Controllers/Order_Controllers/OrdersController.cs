using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.AddressDTO.Location;
using Yumm_Back_End.Models.DTOs.OrderDTO;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._AddressRepository.UserLocationRepository;
using Yumm_Back_End.Repository._OrderRepository;

namespace Yumm_Back_End.Controllers.Order_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository _orderRepo;
        private readonly IUserLocationRepository _userLocaRepo;
        private readonly IMapper _mapper;


        public OrdersController(IMapper mapper,IOrderRepository orderRepo, IUserLocationRepository userLocaRepo)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _userLocaRepo = userLocaRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO createOrderDTO)
        {

            if (createOrderDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            
            var orderObj = _mapper.Map<Order>(createOrderDTO);

            var savedOrder = await _orderRepo.CreateOrder(orderObj);

            var orderedProductsList = createOrderDTO.Products;

            foreach (var product in orderedProductsList)
            {
                var newOrderProduct = new OrdersProducts
                { 
                    OrderId = savedOrder.Id,
                    ProductId = product.ProductId,
                    Quatity = product.Quatity
                };

               var savedOrderProduct = await _orderRepo.AssociateOrderProduct(newOrderProduct);

                foreach (var productExtra in product.OrderProductExtras)
                {
                    var newOrderProductExtra = new OrderProductExtras {

                           OrdersProductsId = savedOrderProduct.Id,
                           ProductExtrasId = productExtra.ProductExtrasId,
                           Quantity = productExtra.Quantity

                    };

                    _orderRepo.AddOrderProductExtra(newOrderProductExtra);
                }

            }



            return StatusCode(201);
        }

        

        [HttpGet("GetRestaurantOrders/{restaurantId:int}")]
        public IActionResult GetRestaurantOrders(int restaurantId)
        {
            var orderList = _orderRepo.GetRestaurantOrders(restaurantId);

            var restaurantOrdersList = new List<ViewOrderDTO>();

            var orderedProductsList = new List<ViewOrderProduct>();


            foreach (var singleOrder in orderList)
            {

                       

                foreach (var orderProduct in singleOrder.OrdersProducts)
                {

                    var orderedProductExtras = _orderRepo.GetProductExtras(orderProduct.Id, orderProduct.Product.Id);

                    var ordersExtraList = new List<ViewOrderProductExtras>();


                    foreach (var extra in orderedProductExtras)
                    {
                        ViewOrderProductExtras orderExtra = new ViewOrderProductExtras
                        {
                            Id = extra.Id,
                            Name = extra.ProductExtras.Name,
                            Description = extra.ProductExtras.Description,
                            Price = extra.ProductExtras.Price,
                        };

                        ordersExtraList.Add(orderExtra);

                    }



                    ViewOrderProduct product = new ViewOrderProduct
                    {
                        Id = orderProduct.Product.Id,
                        Name = orderProduct.Product.Name,
                        Price = orderProduct.Product.Price,
                        Size = orderProduct.Product.Size,
                        Note = orderProduct.Order.Note,

                        OrderExtras = ordersExtraList
                    };

                    orderedProductsList.Add(product);
                }


                ViewOrderDTO order = new ViewOrderDTO
                {
                    Id = singleOrder.Id,
                    
                    OrderStatus = singleOrder.OrderStatus.Name,
                    isPaid = singleOrder.isPaid,
                    Note = singleOrder.Note,
                    RestaurantName = singleOrder.Restaurants.Name,
                    
                    OrdersProducts = orderedProductsList
                };

                restaurantOrdersList.Add(order);

            }

            return Ok(restaurantOrdersList);

        }

        [HttpGet("GetClientOrders/{clientId:int}")]
        public IActionResult GetClientOrders(int clientId)
        {
            var orderList = _orderRepo.GetClientOrders(clientId);

            var clientOrdersList = new List<ViewOrderDTO>();

            var orderedProductsList = new List<ViewOrderProduct>();
            

            foreach (var singleOrder in orderList)
            {

                Console.WriteLine(singleOrder.Id);

                var userlocation = _userLocaRepo.GetUserLocation(singleOrder.Clients.UserId, singleOrder.LocationId);

                UserLocationDTO orderLocation = new UserLocationDTO
                {

                    Name = userlocation.Name,
                    Unit = userlocation.Unit,
                    Description = userlocation.Description,
                    LocationId = userlocation.LocationId,
                    City = userlocation.Location.City.Name,
                    Commune = userlocation.Location.Commune.Name,
                    Neighbourhood = userlocation.Location.Neighbourhood.Name,
                    Street = userlocation.Location.Street.Name,
                    Lat = userlocation.Location.Lat,
                    Lng = userlocation.Location.Lng
                };

                foreach (var orderProduct in singleOrder.OrdersProducts)
                {

                    var orderedProductExtras = _orderRepo.GetProductExtras(orderProduct.Id, orderProduct.Product.Id);

                    var ordersExtraList = new List<ViewOrderProductExtras>();


                    foreach (var extra in orderedProductExtras)
                    {
                        ViewOrderProductExtras orderExtra = new ViewOrderProductExtras
                        {
                            Id = extra.Id,
                            Name = extra.ProductExtras.Name,
                            Description = extra.ProductExtras.Description,
                            Price = extra.ProductExtras.Price,
                        };

                        ordersExtraList.Add(orderExtra);

                    }



                    ViewOrderProduct product = new ViewOrderProduct
                    {
                        Id = orderProduct.Product.Id,
                        Name = orderProduct.Product.Name,
                        Price = orderProduct.Product.Price,
                        Size = orderProduct.Product.Size,
                        Note = orderProduct.Order.Note,

                        OrderExtras = ordersExtraList
                    };

                    orderedProductsList.Add(product);
                }


                ViewOrderDTO order = new ViewOrderDTO
                {
                    Id = singleOrder.Id,
                    TotalAmount = singleOrder.Amount,
                    OrderStatus = singleOrder.OrderStatus.Name,
                    isPaid = singleOrder.isPaid,
                    Note = singleOrder.Note,
                    RestaurantName = singleOrder.Restaurants.Name,
                    ClientName = singleOrder.Clients.FirstName + " " + singleOrder.Clients.LastName,
                    Location = orderLocation,
                    OrdersProducts = orderedProductsList
                };



                clientOrdersList.Add(order);

            }

            return Ok(clientOrdersList);
        }


        [HttpGet]
        public IActionResult GetOrder(int orderId)
        {

            var orderObj = _orderRepo.GetOrder(orderId);

           
            var orderedProductsList = new List<ViewOrderProduct>();

            var userlocation = _userLocaRepo.GetUserLocation(orderObj.Clients.UserId, orderObj.LocationId);

            foreach (var orderProduct in orderObj.OrdersProducts)
            {

                var orderedProductExtras = _orderRepo.GetProductExtras(orderProduct.Id, orderProduct.Product.Id);

                var ordersExtraList = new List<ViewOrderProductExtras>();
               
                foreach (var extra in orderedProductExtras)
                {
                    ViewOrderProductExtras orderExtra = new ViewOrderProductExtras
                    {
                        Id = extra.Id,
                        Name = extra.ProductExtras.Name,
                        Description = extra.ProductExtras.Description,
                        Price = extra.ProductExtras.Price,
                    };

                    ordersExtraList.Add(orderExtra);

                }

                ViewOrderProduct product = new ViewOrderProduct
                {
                    Id = orderProduct.Product.Id,
                    Name = orderProduct.Product.Name,
                    Price = orderProduct.Product.Price,
                    Size = orderProduct.Product.Size,
                    Note = orderProduct.Order.Note,
                 
                    OrderExtras = ordersExtraList
                };

                orderedProductsList.Add(product);
                
            /}


            UserLocationDTO orderLocation = new UserLocationDTO {

                Name = userlocation.Name,
                Unit = userlocation.Unit,
                Description = userlocation.Description,
                LocationId = userlocation.LocationId,
                City = userlocation.Location.City.Name,
                Commune = userlocation.Location.Commune.Name,
                Neighbourhood = userlocation.Location.Neighbourhood.Name,
                Street = userlocation.Location.Street.Name,
                Lat = userlocation.Location.Lat,
                Lng = userlocation.Location.Lng
            };


            ViewOrderDTO order = new ViewOrderDTO 
            {
                Id = orderObj.Id,
                TotalAmount = orderObj.Amount,
                OrderStatus = orderObj.OrderStatus.Name,
                isPaid = orderObj.isPaid,
                Note = orderObj.Note,
                RestaurantName = orderObj.Restaurants.Name,
                ClientName = orderObj.Clients.FirstName +" " + orderObj.Clients.LastName,
                Location = orderLocation,
                OrdersProducts = orderedProductsList
            };

            return Ok(order);
        }



    }

}
