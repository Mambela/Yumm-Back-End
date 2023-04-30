using ImageMagick;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository
{
    public interface IOrderRepository
    {


        Order GetOrder(int id);

        IList<Order> GetRestaurantOrders(int restaurantId,string status);
        IList<Order> GetRestaurantProcessedOrders(int restaurantId,PaginationFilter filter);
        IList<Order> GetRestaurantProcessedOrdersHistoryByDateRange(int restaurantId,PaginationFilter filter,DateTime startDate,DateTime endDate);

        int CountRestaurantOrders(int restaurantId, string status);

        IList<OrderDetails> GetOrdersInRestaurant(int restaurantId, PaginationFilter filter);

        IList<Order> GetClientOrders(string clientId,PaginationFilter pagination);


        int CountClientOrders(string clientId);

        int CountRestaurantDeliveryOrders(int restarauntId);

        int CountRestaurantDeliveryProcessedOrders(int restarauntId);

        int CountRestaurantOrderHistoryByDateRange(int restaurantId,PaginationFilter filter,DateTime startDate,DateTime endDate);

        IList<OrderDetails> GetOrderDetails(int orderId);
        

        Task<bool> CreateOrder(Order order);

        Task<bool> CreateOrderDetails(OrderDetails orderDetails);

        Task<bool> CreateOrderDetails(List<OrderDetails> orderDetails);

        bool UpdateOrder(Order order);

        

        bool Save();
    }
}
