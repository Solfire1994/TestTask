using System.Collections.Generic;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services.Interfaces.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext Context;
        public OrderService (ApplicationDbContext context)
        {
            Context = context;
        }
        public Task<Order> GetOrder()
        {
            Order result = Context.Orders.FirstOrDefault();
            foreach (var item in Context.Orders)
            {
                result = item.Price < result.Price ? result : item;
            }
            return Task.FromResult(result);
        }

        public Task<List<Order>> GetOrders()
        {
            List<Order> result = new();

            foreach (var item in Context.Orders)
            {
                if(item.Quantity > 10) result.Add(item);
            }
            return Task.FromResult(result);
        }
    }
}
