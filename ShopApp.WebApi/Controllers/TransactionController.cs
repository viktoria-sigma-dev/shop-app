using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.UseCases.TransactionCases;
using ShopApp.Application.DTOs.TransactionDTOs;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly GetAllByOrderTransactionUseCase _getAllByOrderTransactionUseCase;
        private readonly GetAllByUserTransactionUseCase _getAllByUserTransactionUseCase;
        public TransactionController(GetAllByOrderTransactionUseCase getAllByOrderTransactionUseCase, GetAllByUserTransactionUseCase getAllByUserTransactionUseCase)
        {
            _getAllByOrderTransactionUseCase = getAllByOrderTransactionUseCase;
            _getAllByUserTransactionUseCase = getAllByUserTransactionUseCase;
        }

        [HttpGet("order-transactions/{orderId}")]
        public async Task<ActionResult<IEnumerable<TransactionResponseDTO>>> GetAllByOrderId(int orderId)
        {
            var transactionList = await _getAllByOrderTransactionUseCase.Execute(orderId);

            return Ok(transactionList);
        }

        [HttpGet("user-transactions/{userId}")]
        public async Task<ActionResult<IEnumerable<TransactionResponseDTO>>> GetAllByUserId(int userId)
        {
            var transactionList = await _getAllByUserTransactionUseCase.Execute(userId);

            return Ok(transactionList);
        }
    }
}