using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.OrderCases
{
    public class DeclineOrderUseCase(IOrderRepository orderRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        private IOrderRepository _orderRepository = orderRepository;
        private ITransactionRepository _transactionRepository = transactionRepository;
        private IUnitOfWork _unitOfWork = unitOfWork;
        private IMapper _mapper = mapper;
        public async Task<OrderResponseDTO> Execute(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await _orderRepository.GetOneFullAsync(id);
                if (order == null) throw new OrderNotFoundException(id);

                order.DeclineOrder();

                await _orderRepository.UpdateOrder(order);

                await _unitOfWork.CommitAsync();
                return _mapper.Map<OrderResponseDTO>(order);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}