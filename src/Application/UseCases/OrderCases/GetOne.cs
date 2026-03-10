using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.OrderCases
{
    public class GetOneOrderUseCase(IOrderRepository orderRepository)
    {
        private IOrderRepository _orderRepository = orderRepository;
        public async Task<Order> Execute(int id)
        {
            var order = await _orderRepository.GetOneAsync(id);
            if (order == null) throw new KeyNotFoundException($"Order with such id {id} does not exist!");

            return order;
        }
    }
}