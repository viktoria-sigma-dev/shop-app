using ShopApp.Domain.Interfaces;
using ShopApp.Application.Exceptions;
using AutoMapper;
using ShopApp.Application.DTOs.TransactionDTOs;

namespace ShopApp.Application.UseCases.TransactionCases
{
    public class GetAllByUserTransactionUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IUserRepository _userRepository = userRepository;
        private IMapper _mapper = mapper;
        public async Task<List<TransactionResponseDTO>> Execute(int userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var transactions = await _orderRepository.GetUserTransactionsAsync(userId);
            return _mapper.Map<List<TransactionResponseDTO>>(transactions);
        }
    }
}