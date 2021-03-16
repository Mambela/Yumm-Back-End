using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Order order);

        public Task<OrdersProducts> AssociateOrderProduct(OrdersProducts orderProducts);

        public bool AddOrderProductExtra(OrderProductExtras orderProductExtras);

        public ICollection<Order> GetOrders();

        public ICollection<OrdersProducts> GetOrderProducts(int orderId);

        public ICollection<Order> GetClientOrders(int ClientId);

        public ICollection<Order> GetRestaurantOrders(int RestaurantId);

        public ICollection<Order> GetDriverOrders(int driverId);

        public ICollection<OrderProductExtras> GetProductExtras(int orderProductId,int productId);

        public Order GetOrder(int orderId);

        public bool UpdateOrder(Order Order);

        public bool Save();

    }
}
