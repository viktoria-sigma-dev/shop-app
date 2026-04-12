using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.UseCases.OrderCases;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Enums;

namespace ShopApp.WebApi.Controllers
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
        public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] CreateOrderDTO orderDto)
        {
            var order = await _createOrderUseCase.Execute(orderDto);

            return Ok(order);
        }
        [HttpPatch("{orderId}/decline")]
        public async Task<ActionResult<OrderResponseDTO>> Decline(int orderId)
        {
            var order = await _declineOrderUseCase.Execute(orderId);

            return Ok(order);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAll()
        {
            var orders = await _getAllOrderUseCase.Execute();

            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponseDTO>> GetOne(int orderId)
        {
            var order = await _getOneOrderUseCase.Execute(orderId);

            return Ok(order);
        }
        [HttpPatch("{orderId}/pack")]
        public async Task<ActionResult<OrderResponseDTO>> PackOrder(int orderId)
        {
            var order = await _updateStatusOrderUseCase.Execute(orderId, OrderStatus.Packed);

            return Ok(order);
        }
        [HttpPatch("{orderId}/deliver")]
        public async Task<ActionResult<OrderResponseDTO>> DeliverOrder(int orderId)
        {
            var order = await _updateStatusOrderUseCase.Execute(orderId, OrderStatus.Delivered);

            return Ok(order);
        }
    }
}