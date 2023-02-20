using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int CountClientOrders(string clientId)
        {
            return dbContext.Orders
               .Include(o => o.OrderedByUser)
               .Where(c => c.OrderedByUserId == clientId)
               .Count();
        }

        public int CountRestaurantDeliveryOrders(int restarauntId)
        {
            return dbContext.Orders
                 .Where(
                    o => o.OrderDetails.Select(o => o.Product.MenuGroup.Menu.RestaurantId).Contains(restarauntId) &&  
                    (o.OrderStatus.ToUpper().Trim() != Status.NEW && o.OrderStatus != Status.AWAITINGPAYMENT && o.OrderStatus != Status.PAID)).Count();
        }

        public int CountRestaurantOrders(int restaurantId, string status)
        {
            return dbContext.Orders
               .Where(o => o.OrderDetails.Select(o => o.Product.MenuGroup.Menu.RestaurantId).Contains(restaurantId)).Count();
        }

        public async Task<bool> CreateOrder(Order order)
        {
            await dbContext.Orders.AddAsync(order);

            return Save();
        }

        public async Task<bool> CreateOrderDetails(OrderDetails orderDetails)
        {
            await dbContext.OrderDetails.AddAsync(orderDetails);

            return Save();
        }

        public async Task<bool> CreateOrderDetails(List<OrderDetails> orderDetails)
        {
            await dbContext.OrderDetails.AddRangeAsync(orderDetails);  

            return Save();
        }

        public IList<Order> GetClientOrders(string clientId, PaginationFilter filter)
        {
            return dbContext.Orders
                .Include(o => o.OrderedByUser)
                .Where(c => c.OrderedByUserId == clientId)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }

        public Order GetOrder(int id)
        {
            return dbContext.Orders
                 .Include(o => o.OrderDetails)
                 .FirstOrDefault(o => o.Id == id);
        }

        public IList<OrderDetails> GetOrderDetails(int orderId)
        {
            
            return dbContext.OrderDetails
                .Where(o => o.OrderId == orderId)
                .ToList();
        }

        public IList<OrderDetails> GetOrdersInRestaurant(int restaurantId,PaginationFilter filter)
        {
        
            return dbContext.OrderDetails
                .Include(p => p.Product)
                .Include(o => o.Order)
                .Include(c => c.Order).ThenInclude(c => c.OrderedByUser)
                .Include(c => c.Order).ThenInclude(c => c.ProcessedByUser)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Where(r => r.Product.MenuGroup.Menu.RestaurantId == restaurantId)
                .ToList();
        }

        public IList<Order> GetRestaurantOrders(int restaurantId, string status)
        {
            return dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.OrderDetails).ThenInclude(p =>p.Product)
                .Include(o => o.OrderedByUser).ThenInclude(c =>c.Client)
                .Where(o => o.OrderDetails.Select(o => o.Product.MenuGroup.Menu.RestaurantId).Contains(restaurantId) && o.OrderStatus.ToLower().Trim() == status.ToLower().Trim())
                .ToList();
                  
        }

        public IList<Order> GetRestaurantOrdersByStatus(int restaurantId, PaginationFilter filter)
        {
           return dbContext.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.OrderDetails).ThenInclude(p => p.Product)
                .Include(o => o.OrderedByUser).ThenInclude(c => c.Client)
                .Where(o => o.OrderDetails.Select(o => o.Product.MenuGroup.Menu.RestaurantId).Contains(restaurantId)
                
                && o.OrderStatus.ToUpper().Trim() != Status.NEW && o.OrderStatus!=Status.AWAITINGPAYMENT && o.OrderStatus !=Status.PAID)
                .Take(filter.PageSize)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .ToList();
        }

        public bool Save()
        {
            return dbContext.SaveChanges() >= 0;
        }

        public bool UpdateOrder(Order order)
        {
            dbContext.Orders.Update(order);

            return Save();
        }
 
    }
}
