using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class GetAllOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IMapper _mapper = mapper;
        public async Task<List<OrderResponseDTO>> Execute()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderResponseDTO>>(orders); 
        }
    }
}