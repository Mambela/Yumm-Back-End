using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
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

        public IList<Order> GetClientOrders(string clientId)
        {
            return dbContext.Orders
                .Include(o => o.OrderedByUser)
                .Where(c => c.OrderedByUserId == clientId)
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

        public IList<Order> GetRestaurantOrders(int restaurantId)
        {
            throw new System.NotImplementedException();

            
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
