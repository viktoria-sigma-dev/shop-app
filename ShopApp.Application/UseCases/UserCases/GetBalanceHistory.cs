using AutoMapper;
using ShopApp.Application.DTOs.TransactionDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetBalanceHistoryUserUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        public IOrderRepository _orderRepository = orderRepository;
        public IUserRepository _userRepository = userRepository;
        public IMapper _mapper = mapper;

        public async Task<List<TransactionResponseDTO>> Execute(int userId)
        {
            var user = await _userRepository.GetOneAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var transactions = await _orderRepository.GetUserTransactionsAsync(userId);

            return _mapper.Map<List<TransactionResponseDTO>>(transactions);
        }
    }
}