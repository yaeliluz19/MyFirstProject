﻿using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstProject.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // POST: OrdersController/Create
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDTO orderItemDTO)
        {
            try
            {
                Order order =  _mapper.Map<OrderDTO, Order>(orderItemDTO);
                Order  order2 = await _orderService.CreateOrder(order);
                OrderDTO order3 = _mapper.Map<Order,OrderDTO>(order2);

                if (order3 != null)
                    return Ok(order3);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
