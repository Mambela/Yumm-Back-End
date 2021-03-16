using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.OrderDTO;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Repository._OrderRepository.OrderStatusRepository;

namespace Yumm_Back_End.Controllers.Order_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusRepository _orderStatusRepo;

        private IMapper _mapper;


        public OrderStatusController(IOrderStatusRepository orderStatusRepo, IMapper mapper)
        {
            _mapper = mapper;
            _orderStatusRepo = orderStatusRepo;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrderStatusAsync(OrderStatusDTO orderStatusDTO)
        {
            if (orderStatusDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            if (_orderStatusRepo.StatusExists(orderStatusDTO.Name))
            {
                ModelState.AddModelError("", $"The status you are creating already exists");
                return StatusCode(500, ModelState);
            }

            var orderStatusObj = _mapper.Map<OrderStatus>(orderStatusDTO);


            if (!await _orderStatusRepo.CreateOrderStatus(orderStatusObj))
            {
                ModelState.AddModelError("", $"An error occurred while creating a status {orderStatusObj.Name}");

                return StatusCode(500, ModelState);
            }


            return StatusCode(201,orderStatusObj);
        }

        [HttpGet]
        public IActionResult GetOrderStatuses()
        {
            var orderStatusList = _orderStatusRepo.GetOrderStatuses();

            var orderStatuses = new List<OrderStatus>();

            foreach (var status in orderStatusList)
            {
                orderStatuses.Add(_mapper.Map<OrderStatus>(status));
            }

            return Ok(orderStatuses);

        }

        [HttpGet("{statusId:int}",Name = "GetOrderStatus")]
        public IActionResult GetOrderStatus(int statusId)
        {
            if (!_orderStatusRepo.StatusExists(statusId))
            {
                ModelState.AddModelError("", $"Status not found ");
                return StatusCode(404, ModelState);
            }

            var statusObj = _orderStatusRepo.GetOrderStatus(statusId);

            var status = _mapper.Map<OrderStatusDTO>(statusObj);

            return Ok(status);
        }


        [HttpDelete]
        public IActionResult DeleteOrderStatus(int statusId)
        { 
            var statusObj = _orderStatusRepo.GetOrderStatus(statusId);

            if(statusObj == null)
            {
                return NotFound();
             }
            

            if (!_orderStatusRepo.DeleteOrderStatus(statusObj))
             {
                ModelState.AddModelError("", $"Error ocurred while trying to delete {statusObj.Name}");
                return StatusCode(500, ModelState);
              }


            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateOrderStatus(OrderStatusDTO statusDTO, int orderId)
        {
            if (statusDTO == null || statusDTO.Id != orderId)
               {
                return BadRequest();
               }


                if (!_orderStatusRepo.StatusExists(orderId))
                {
                    return NotFound();
                }


            var status = _mapper.Map<OrderStatus>(statusDTO);

            if(!_orderStatusRepo.UpdateOrderStatus(status))
            {
                ModelState.AddModelError("", $"Error ocurred while trying to update {status.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();


        }




    }
}
