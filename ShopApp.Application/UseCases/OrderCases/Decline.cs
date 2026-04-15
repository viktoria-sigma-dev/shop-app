using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class DeclineOrderUseCase(IOrderRepository orderRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<OrderResponseDTO> Execute(int id)
        {
            await unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await orderRepository.GetOneFullAsync(id);
                if (order == null) throw new OrderNotFoundException(id);

                order.DeclineOrder();

                await orderRepository.UpdateOrder(order);

                await unitOfWork.CommitAsync();
                return mapper.Map<OrderResponseDTO>(order);
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}