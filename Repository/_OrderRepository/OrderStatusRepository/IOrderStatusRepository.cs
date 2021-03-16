using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository.OrderStatusRepository
{
    public interface IOrderStatusRepository
    {

        public Task<bool> CreateOrderStatus(OrderStatus orderStatus);


        public bool StatusExists(String statusName);

        public bool StatusExists(int statusId);

        public ICollection<OrderStatus> GetOrderStatuses();

        public OrderStatus GetOrderStatus(int orderStatusId);

        public bool DeleteOrderStatus(OrderStatus orderStatus);

        public bool UpdateOrderStatus(OrderStatus orderStatus);

        public bool Save();
    }
}
