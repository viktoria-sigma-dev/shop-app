using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class PackOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        public async Task<OrderResponseDTO> Execute(int id)
        {
            var order = await orderRepository.GetOneWithOrderItemsAsync(id);
            if (order == null) throw new OrderNotFoundException(id);

            order.PackOrder();
            var updatedOrder = await orderRepository.UpdateOrder(order);
            return mapper.Map<OrderResponseDTO>(updatedOrder);
        }
    }
}