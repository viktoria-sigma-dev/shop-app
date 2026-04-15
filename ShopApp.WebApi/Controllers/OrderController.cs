using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.UseCases.OrderCases;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(
        CreateOrderUseCase createOrderUseCase,
        DeclineOrderUseCase declineOrderUseCase,
        GetAllOrderUseCase getAllOrderUseCase,
        GetOneOrderUseCase getOneOrderUseCase,
        PackOrderUseCase packOrderUseCase,
        DeliverOrderUseCase deliverOrderUseCase
        ) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] CreateOrderDTO orderDto)
        {
            var order = await createOrderUseCase.Execute(orderDto);

            return Ok(order);
        }
        [HttpPatch("{orderId}/decline")]
        public async Task<ActionResult<OrderResponseDTO>> Decline(int orderId)
        {
            var order = await declineOrderUseCase.Execute(orderId);

            return Ok(order);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAll()
        {
            var orders = await getAllOrderUseCase.Execute();

            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponseDTO>> GetOne(int orderId)
        {
            var order = await getOneOrderUseCase.Execute(orderId);

            return Ok(order);
        }
        [HttpPatch("{orderId}/pack")]
        public async Task<ActionResult<OrderResponseDTO>> PackOrder(int orderId)
        {
            var order = await packOrderUseCase.Execute(orderId);

            return Ok(order);
        }
        [HttpPatch("{orderId}/deliver")]
        public async Task<ActionResult<OrderResponseDTO>> DeliverOrder(int orderId)
        {
            var order = await deliverOrderUseCase.Execute(orderId);

            return Ok(order);
        }
    }
}