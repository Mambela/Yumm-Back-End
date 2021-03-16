using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository.OrderStatusRepository
{
    public class OrderStatusRepository :IOrderStatusRepository
    {
        ApplicationDbContext _dbContext;

        public OrderStatusRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateOrderStatus(OrderStatus orderStatus)
        {

                await _dbContext.OrderStatuses.AddAsync(orderStatus);

            return Save();
        }

        public bool DeleteOrderStatus(OrderStatus orderStatus)
        {
            _dbContext.OrderStatuses.Remove(orderStatus);

            return Save();
        }

        public OrderStatus GetOrderStatus(int orderStatusId)
        {
            return _dbContext.OrderStatuses.FirstOrDefault(s=>s.Id == orderStatusId);
        }

        public ICollection<OrderStatus> GetOrderStatuses()
        {
            return _dbContext.OrderStatuses.ToList();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool StatusExists(string statusName)
        {
            return _dbContext.OrderStatuses.Any(s => s.Name.Trim().ToLower() == statusName.Trim().ToLower());
        }

        public bool StatusExists(int statusId)
        {
            return _dbContext.OrderStatuses.Any(s => s.Id == statusId);
        }

        public bool UpdateOrderStatus(OrderStatus orderStatus)
        {
            _dbContext.OrderStatuses.Update(orderStatus);

            return Save();
        }
    }
}
