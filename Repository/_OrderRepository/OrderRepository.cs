using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Repository._OrderRepository
{
    public class OrderRepository:IOrderRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);

            if (Save())
            {
                return order;
            }

            return null;            
        }

        public bool AddOrderProductExtra(OrderProductExtras orderProductExtras)
        {
            _dbContext.OrderProductExtras.Add(orderProductExtras);

            return Save();
        }

        public ICollection<Order> GetClientOrders(int ClientId)
        {

            return _dbContext.Orders.Include(o => o.OrdersProducts).ThenInclude(o => o.Product)
                                    .Include(o => o.OrderStatus)
                                    .Include(o => o.Restaurants)
                                    .Include(o => o.Clients)
                                    .Include(o => o.Clients.User)
                                    .Include(o => o.Locations).Where(c =>c.ClientId == ClientId).ToList();
            
        }

        public ICollection<Order> GetDriverOrders(int driverId)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int orderId)
        {

            return _dbContext.Orders.Include(o => o.OrdersProducts).ThenInclude(o =>o.Product)
                                    .Include(o => o.OrderStatus)
                                    .Include(o => o.Restaurants)
                                    .Include(o => o.Clients)
                                    .Include(o => o.Clients.User)
                                    .Include(o => o.Locations).FirstOrDefault(o => o.Id == orderId);
             
        }

        public ICollection<OrdersProducts> GetOrderProducts(int orderId)
        {
          return _dbContext.OrdersProducts.Include(p => p.Product)
                                          .Include(p=>p.Product.ProductsExtras).Where(o =>o.OrderId == orderId).ToList();
        }

        public ICollection<Order> GetOrders()
        {
            return _dbContext.Orders.Include(o => o.OrdersProducts).ThenInclude(o => o.Product)
                                   .Include(o => o.OrderStatus).ToList();
        }

        public ICollection<Order> GetRestaurantOrders(int RestaurantId)
        {
            return _dbContext.Orders.Include(o => o.OrdersProducts).ThenInclude(o => o.Product)
                                   .Include(o => o.OrderStatus)
                                   .Include(o => o.Restaurants)
                                   .Include(o => o.Clients)
                                   .Include(o => o.Clients.User)
                                   .Include(o => o.Locations).Where(c => c.RestaurantId == RestaurantId).ToList();
        }

        public bool UpdateOrder(Order Order)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public async Task<bool> CreateOrderToProduct(OrdersProducts orderProducts)
        {
            await _dbContext.OrdersProducts.AddAsync(orderProducts);

            return Save();
        }

        public async Task<OrdersProducts> AssociateOrderProduct(OrdersProducts orderProducts)
        {

            await _dbContext.OrdersProducts.AddAsync(orderProducts);

            if (Save())
            {
                return orderProducts;
            }

            return null;
        }

        public ICollection<OrderProductExtras> GetProductExtras(int orderProductId, int productId)
        {
            return _dbContext.OrderProductExtras.Include(op => op.ProductExtras).Where(o => o.OrdersProductsId == orderProductId).ToList();
     
        }
    }
}
