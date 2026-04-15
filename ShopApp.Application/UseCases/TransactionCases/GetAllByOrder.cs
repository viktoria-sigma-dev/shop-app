using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using ShopApp.Application.DTOs.TransactionDTOs;
using AutoMapper;

namespace ShopApp.Application.UseCases.TransactionCases
{
    public class GetAllByOrderTransactionUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        public async Task<List<TransactionResponseDTO>> Execute(int orderId)
        {
            var order = await orderRepository.GetOneWithTransactionsAsync(orderId);
            if (order == null) throw new OrderNotFoundException(orderId);

            return mapper.Map<List<TransactionResponseDTO>>(order.Transactions);
        }
    }
}