using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using ShopApp.Application.DTOs.TransactionDTOs;
using AutoMapper;

namespace ShopApp.Application.UseCases.TransactionCases
{
    public class GetAllByOrderTransactionUseCase(ITransactionRepository transactionRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        private ITransactionRepository _transactionRepository = transactionRepository;
        private IOrderRepository _orderRepository = orderRepository;
        private IMapper _mapper = mapper;
        public async Task<List<TransactionResponseDTO>> Execute(int orderId)
        {
            var order = await _orderRepository.GetOneWithTransactionsAsync(orderId);
            if (order == null) throw new OrderNotFoundException(orderId);

            return _mapper.Map<List<TransactionResponseDTO>>(order.Transactions);
        }
    }
}