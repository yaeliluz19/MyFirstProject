using Microsoft.Extensions.Logging;
using MyFirstProject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepositories _OrderRepositories;
        private IProductService _productService;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrderRepositories OrderRepositories, IProductService productService, ILogger<OrderService> logger)
        {
            _OrderRepositories = OrderRepositories;
            _productService = productService;
            _logger = logger;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            int sum = 0;
            foreach (OrderItem item in order.OrderItems)
            {
                Product product =await _productService.GetById(item.ProductId);
                sum += product.Price * item.Quantity;
            }
            if(sum == order.OrderSum) {             
                return await _OrderRepositories.CreateOrder(order);
}
            else
            {

                order.OrderSum = sum;
                _logger.LogError($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
                _logger.LogInformation($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
                return await _OrderRepositories.CreateOrder(order);

            }
        }
    }
}
