using MyFirstProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private Market326354982Context _market326354982;

        public OrderRepositories(Market326354982Context market326354982Context)
        {
            _market326354982 = market326354982Context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                await _market326354982.Orders.AddAsync(order);
                await _market326354982.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
