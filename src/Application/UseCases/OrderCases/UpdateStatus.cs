using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Enums;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.OrderCases
{
    public class UpdateStatusOrderUseCase(IOrderRepository orderRepository)
    {
        private IOrderRepository _orderRepository = orderRepository;
       public async Task<Order> Execute(int id, OrderStatus orderStatus)
        {
            var order = await _orderRepository.GetOneAsync(id);
            if (order == null) throw new KeyNotFoundException($"Order with such id {id} was not found");
            if (orderStatus == OrderStatus.Packed & order.Status != OrderStatus.Initiated) throw new BadHttpRequestException("Order in such a status cannot be packed.");
            if (orderStatus == OrderStatus.Delivered & order.Status != OrderStatus.Packed) throw new BadHttpRequestException("Order in such a status cannot be delivered.");
            order.Status = orderStatus;
            return await _orderRepository.UpdateOrder(order);
        } 
    }
}