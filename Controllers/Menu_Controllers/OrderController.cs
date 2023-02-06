using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.OrderDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.HelperClasses;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Repository._OrderRepository;
using Yumm_Back_End.Services.Logger;

namespace Yumm_Back_End.Controllers.Menu_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILoggerService loggerService;

        public OrderController(IOrderRepository _orderRepository,ILoggerService _loggerService)
        {
            orderRepository = _orderRepository;
            loggerService = _loggerService;
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
    }
}
