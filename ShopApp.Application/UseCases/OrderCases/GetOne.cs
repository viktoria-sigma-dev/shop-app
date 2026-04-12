using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class GetOneOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IMapper _mapper = mapper;
        public async Task<OrderResponseDTO> Execute(int id)
        {
            var order = await _orderRepository.GetOneWithOrderItemsAsync(id);
            if (order == null) throw new OrderNotFoundException(id);

            return _mapper.Map<OrderResponseDTO>(order);
        }
    }
}