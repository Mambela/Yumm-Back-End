using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Helper;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.DTOs.OrderDTO;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.HelperClasses;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._OrderRepository;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Services.PaginationService;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILoggerService loggerService;
        private readonly IUriService uriService;

        public OrderController(IOrderRepository _orderRepository,ILoggerService _loggerService, IUriService _uriService)
        {
            orderRepository = _orderRepository;
            loggerService = _loggerService;
            uriService = _uriService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(NewOrderDTO newOrderDTO)
        {
            try
            {

                if (newOrderDTO == null)
                {
                    return BadRequest();
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //# CREATE A NEW ORDER


                var order = new Order
                {
                    TakenOderDate = DateTime.UtcNow,
                    TotalAmount = newOrderDTO.OrderDetails.Select(o => o.Amount * o.NoOfServings).Sum(),
                    Note =newOrderDTO.Note,
                    ClientAddressId = newOrderDTO.LocationId,
                    OrderStatus =StatusEnum.New.ToString(),

                    OrderedByUserId = newOrderDTO.OrderedByUserId,
                };


                if (!await orderRepository.CreateOrder(order))
                {
                    ModelState.AddModelError("error", "error while trying to create a new order");
                    return BadRequest(ModelState);
                }



                //#CREATE ORDER DETAILS

                var orderDetails = new List<OrderDetails>();


                foreach (var orderDetail in newOrderDTO.OrderDetails)
                {

                    orderDetails.Add(
                        new OrderDetails 
                            {
                            Amount = orderDetail.Amount,
                            NoOfServing = orderDetail.NoOfServings,
                            OrderId = order.Id,
                            ProductId = orderDetail.ProductId,
                            Size = orderDetail.Size,
                            Note = orderDetail.Note
                            }
                        );
                }


                if (!await orderRepository.CreateOrderDetails(orderDetails))
                {
                    ModelState.AddModelError("error", "an error occurred while attempting to create ");

                    return BadRequest(ModelState);
                }

                
               return StatusCode(StatusCodes.Status201Created,newOrderDTO);
            }
            catch (Exception ex)
            {
                loggerService.LogError("New order error " + ex.Message);
                ModelState.AddModelError("error", "error while trying to create a new order, please check the logs.");
                return BadRequest();
            }
        }


        [HttpGet("RestaurantOrders")]
        public IActionResult GetRestaurantOrders(int restaurantId,string status, [FromQuery] PaginationFilter pagination)
        {
            try
            {

                TimeEstimator estmator = new TimeEstimator();
                var restaurantOrdersListObj = orderRepository.GetRestaurantOrders(restaurantId,status);
                var route = Request.Path.Value;
                var orderList = new List<ViewOrderDTO>();

                foreach (var order in restaurantOrdersListObj)
                {
                    var orderDetails = new List<ViewOrderDetails>();

                    var newOrder = new ViewOrderDTO
                    {
                        Id = order.Id,
                        Note = order.Note,
                        OrderedDate = order.TakenOderDate,
                        Status = order.OrderStatus,
                        ClientName = order.OrderedByUser.Client.FirstName +" "+ order.OrderedByUser.Client.LastName,
                        PhoneNumber = order.OrderedByUser.PhoneNumber,
                        TotalAmount = order.TotalAmount,
                        DueTime = estmator.DueAt(order),
                        NumberOfProducts = order.OrderDetails.Count
                    };


                    

                    foreach (var orderItem in order.OrderDetails)
                    {

                        orderDetails.Add(
                            new ViewOrderDetails { 
                                Amount = orderItem.Amount,
                                Id = orderItem.Id,
                                Note = orderItem.Note,
                                PrepTime = orderItem.Product.Duration,
                                Quantity = orderItem.NoOfServing,
                                ProductName = orderItem.Product.Name,
                                Size = orderItem.Size,
                                ImageLink = orderItem.Product.ImagePath,
                                
                            }

                            );
                    newOrder.OrderDetails = orderDetails;
                    }
                    orderList.Add(newOrder);
                }





                int totalRecords = orderRepository.CountRestaurantOrders(restaurantId, status);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewOrderDTO>(orderList, validFilter, totalRecords, uriService, route);

                return Ok(pageResponse);

            }
            catch (Exception ex)
            {
                loggerService.LogError("Error fetching " + ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("RestaurantOrdersDelivery")]
        public IActionResult GetRestaurantOrdersForDelivery(int restaurantId, [FromQuery] PaginationFilter filters) {

            try
            {

                TimeEstimator estmator = new TimeEstimator();
                var restaurantOrdersListObj = orderRepository.GetRestaurantProcessedOrders(restaurantId,filters);
                var route = Request.Path.Value;
                var orderList = new List<ViewOrderDTO>();

                foreach (var order in restaurantOrdersListObj)
                {
                    var orderDetails = new List<ViewOrderDetails>();

                    var newOrder = new ViewOrderDTO
                    {
                        Id = order.Id,
                        Note = order.Note,
                        OrderedDate = order.TakenOderDate,
                        Status = order.OrderStatus,
                        ClientName = order.OrderedByUser.Client.FirstName + " " + order.OrderedByUser.Client.LastName,
                        PhoneNumber = order.OrderedByUser.PhoneNumber,
                        TotalAmount = order.TotalAmount,
                        NumberOfProducts = order.OrderDetails.Count,
                        DueTime = estmator.DueAt(order)
                    };




                    foreach (var orderItem in order.OrderDetails)
                    {

                        orderDetails.Add(
                            new ViewOrderDetails
                            {
                                Amount = orderItem.Amount,
                                Id = orderItem.Id,
                                Note = orderItem.Note,
                                PrepTime = orderItem.Product.Duration,
                                Quantity = orderItem.NoOfServing,
                                ProductName = orderItem.Product.Name,
                                Size = orderItem.Size,
                                ImageLink = orderItem.Product.ImagePath,
                            }

                            );
                        newOrder.OrderDetails = orderDetails;
                    }
                    orderList.Add(newOrder);
                }


                int totalRecords = orderRepository.CountRestaurantDeliveryOrders(restaurantId);

                var validFilter = new PaginationFilter(filters.PageNumber, filters.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewOrderDTO>(orderList, validFilter, totalRecords, uriService, route);

                return Ok(pageResponse);

            }
            catch (Exception ex)
            {
                loggerService.LogError(""+ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("ClientOrders")]
        public IActionResult ClientOrders(string clientId, string status, [FromQuery] PaginationFilter pagination)
        {
            try
            {
                var clientOrderObjList = orderRepository.GetClientOrders(clientId,pagination);
                var clientOrdersList = new List<ViewOrderDTO>();
                var route = Request.Path.Value;
                var orderList = new List<ViewOrderDTO>();

                foreach (var order in clientOrderObjList)
                {
                    var orderDetails = new List<ViewOrderDetails>();

                    var newOrder = new ViewOrderDTO
                    {
                        Id = order.Id,
                        Note = order.Note,
                        OrderedDate = order.TakenOderDate,
                        Status = order.OrderStatus,
                        TotalAmount = order.TotalAmount,
                        NumberOfProducts = order.OrderDetails.Count
                    };




                    foreach (var orderItem in order.OrderDetails)
                    {

                        orderDetails.Add(
                            new ViewOrderDetails
                            {
                                Amount = orderItem.Amount,
                                Id = orderItem.Id,
                                Note = orderItem.Note,
                                PrepTime = orderItem.Product.Duration,
                                Quantity = orderItem.NoOfServing,
                                ProductName = orderItem.Product.Name,
                                Size = orderItem.Size
                            }

                            );
                        newOrder.OrderDetails = orderDetails;
                    }

                    orderList.Add(newOrder);
                }


                int totalRecords = orderRepository.CountClientOrders(clientId);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewOrderDTO>(clientOrdersList, validFilter, totalRecords, uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error fetching " + ex.Message);
                return BadRequest();
            }

        }

        [HttpGet("DelivererOrder")]
        public IActionResult DelivererOrders(string delivererId, string status, [FromQuery] PaginationFilter pagination)
        {
            try
            {


                return null;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("OrderHistory")]
        public IActionResult GetRestaurantHistory(int restaurantId, DateTime startDate, DateTime endDate, [FromQuery] PaginationFilter pagination)
        {
            try
            {
                var ordersByHistory = orderRepository.GetRestaurantProcessedOrdersHistoryByDateRange(restaurantId, pagination, startDate, endDate);
                TimeEstimator estmator = new TimeEstimator();
                var route = Request.Path.Value;
                var orderList = new List<ViewOrderDTO>();

                foreach (var order in ordersByHistory)
                {
                    var orderDetails = new List<ViewOrderDetails>();

                    var newOrder = new ViewOrderDTO
                    {
                        Id = order.Id,
                        Note = order.Note,
                        OrderedDate = order.TakenOderDate,
                        Status = order.OrderStatus,
                        ClientName = order.OrderedByUser.Client.FirstName + " " + order.OrderedByUser.Client.LastName,
                        PhoneNumber = order.OrderedByUser.PhoneNumber,
                        TotalAmount = order.TotalAmount,
                        DueTime = estmator.DueAt(order),
                        NumberOfProducts = order.OrderDetails.Count
                    };




                    foreach (var orderItem in order.OrderDetails)
                    {

                        orderDetails.Add(
                            new ViewOrderDetails
                            {
                                Amount = orderItem.Amount,
                                Id = orderItem.Id,
                                Note = orderItem.Note,
                                PrepTime = orderItem.Product.Duration,
                                Quantity = orderItem.NoOfServing,
                                ProductName = orderItem.Product.Name,
                                Size = orderItem.Size,
                                ImageLink = orderItem.Product.ImagePath,

                            }

                            );
                        newOrder.OrderDetails = orderDetails;
                    }
                    orderList.Add(newOrder);
                }





                int totalRecords = orderRepository.CountRestaurantOrderHistoryByDateRange(restaurantId,pagination,startDate,endDate);

                var validFilter = new PaginationFilter(pagination.PageNumber, pagination.PageSize);

                var pageResponse = PaginationHelper.CreatePageResponse<ViewOrderDTO>(orderList, validFilter, totalRecords, uriService, route);

                return Ok(pageResponse);
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error fetching " + ex.Message);
                return BadRequest("Falied to retrieve Restaraunt History");
            }
        }


        [HttpPatch]
        public IActionResult ChangeOrderStatus(string status, int orderId)
        {
            try
            {
                var orderObj = orderRepository.GetOrder(orderId);

                if (orderObj == null)
                {
                    return BadRequest("Order not found.");
                }

                orderObj.OrderStatus = Status.GetStatus(status);

                if (orderObj.OrderStatus == null)
                    throw new Exception("incorrect status provided");

                orderRepository.UpdateOrder(orderObj);

                return Ok();
            }
            catch (Exception ex)
            {
                loggerService.LogError("Error updating order status " + ex.Message);
                return BadRequest("Failed to change order status");
            }
        }
    }
}
