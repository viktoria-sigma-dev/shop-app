using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApp.src.Application.DTOs.OrderDTOs;
using UsersApp.src.Application.UseCases.OrderCases;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;

namespace UsersApp.src.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly DeclineOrderUseCase _declineOrderUseCase;
        private readonly GetAllOrderUseCase _getAllOrderUseCase;
        private readonly GetOneOrderUseCase _getOneOrderUseCase;
        private readonly UpdateStatusOrderUseCase _updateStatusOrderUseCase;
        public OrderController(
            CreateOrderUseCase createOrderUseCase,
            DeclineOrderUseCase declineOrderUseCase,
            GetAllOrderUseCase getAllOrderUseCase,
            GetOneOrderUseCase getOneOrderUseCase,
            UpdateStatusOrderUseCase updateStatusOrderUseCase
        )
        {
            _createOrderUseCase = createOrderUseCase;
            _declineOrderUseCase = declineOrderUseCase;
            _getAllOrderUseCase = getAllOrderUseCase;
            _getOneOrderUseCase = getOneOrderUseCase;
            _updateStatusOrderUseCase = updateStatusOrderUseCase;
        }

        [HttpPost()]
        public async Task<ActionResult<OrderDTO>> Create([FromBody] CreateOrderDTO orderDto)
        {
            var order = await _createOrderUseCase.Execute(orderDto);
            var orderDtoResponse = new OrderDTO
            {
                OrderId = order.OrderId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
            };
            return Ok(orderDtoResponse);
        }
        [HttpPatch("{orderId}/decline")]
        public async Task<ActionResult<OrderDTO>> Decline(int orderId)
        {
            var order = await _declineOrderUseCase.Execute(orderId);
            var orderDtoResponse = new OrderDTO
            {
                OrderId = order.OrderId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
            };
            return Ok(orderDtoResponse);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll()
        {
            var orders = await _getAllOrderUseCase.Execute();
            var ordersDtoResponse = orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt,
            });
            return Ok(ordersDtoResponse);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOne(int orderId)
        {
            var order = await _getOneOrderUseCase.Execute(orderId);
            var orderDtoResponse = new OrderDTO
            {
                OrderId = order.OrderId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
            };
            return Ok(orderDtoResponse);
        }
        [HttpPatch("{orderId}/pack")]
        public async Task<ActionResult<OrderDTO>> PackOrder(int orderId)
        {
            var order = await _updateStatusOrderUseCase.Execute(orderId, OrderStatus.Packed);
            var orderDtoResponse = new OrderDTO
            {
                OrderId = order.OrderId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
            };
            return Ok(orderDtoResponse);
        }
        [HttpPatch("{orderId}/deliver")]
        public async Task<ActionResult<OrderDTO>> DeliverOrder(int orderId)
        {
            var order = await _updateStatusOrderUseCase.Execute(orderId, OrderStatus.Delivered);
            var orderDtoResponse = new OrderDTO
            {
                OrderId = order.OrderId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
            };
            return Ok(orderDtoResponse);
        }
    }
}