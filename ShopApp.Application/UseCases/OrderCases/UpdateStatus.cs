using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Enums;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class UpdateStatusOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IMapper _mapper = mapper;
        public async Task<OrderResponseDTO> Execute(int id, OrderStatus orderStatus)
        {
            var order = await _orderRepository.GetOneWithOrderItemsAsync(id);
            if (order == null) throw new OrderNotFoundException(id);

            order.ChangeStatus(orderStatus);
            var updatedOrder = await _orderRepository.UpdateOrder(order);
            return _mapper.Map<OrderResponseDTO>(updatedOrder);
        }
    }
}