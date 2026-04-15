using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.UseCases.TransactionCases;
using ShopApp.Application.DTOs.TransactionDTOs;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(GetAllByOrderTransactionUseCase getAllByOrderTransactionUseCase, GetAllByUserTransactionUseCase getAllByUserTransactionUseCase) : ControllerBase
    {
        [HttpGet("order-transactions/{orderId}")]
        public async Task<ActionResult<IEnumerable<TransactionResponseDTO>>> GetAllByOrderId(int orderId)
        {
            var transactionList = await getAllByOrderTransactionUseCase.Execute(orderId);

            return Ok(transactionList);
        }

        [HttpGet("user-transactions/{userId}")]
        public async Task<ActionResult<IEnumerable<TransactionResponseDTO>>> GetAllByUserId(int userId)
        {
            var transactionList = await getAllByUserTransactionUseCase.Execute(userId);

            return Ok(transactionList);
        }
    }
}