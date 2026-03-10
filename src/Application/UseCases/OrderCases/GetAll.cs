using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.OrderCases
{
    public class GetAllOrderUseCase(IOrderRepository orderRepository)
    {
        private IOrderRepository _orderRepository = orderRepository;
        public Task<List<Order>> Execute()
        {
            return _orderRepository.GetAllAsync();
        }
    }
}