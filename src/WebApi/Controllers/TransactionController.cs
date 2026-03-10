using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using UsersApp.src.Application.UseCases.TransactionCases;
using UsersApp.src.Application.DTOs.TransactionDTOs;

namespace UsersApp.src.WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllByOrderId(int orderId)
        {
            var transactionList = await _getAllByOrderTransactionUseCase.Execute(orderId);
            var listResponse = transactionList.Select(t => new TransactionDTO
            {
                TransactionId = t.TransactionId,
                CreatedAt = t.CreatedAt,
                Status = t.Status.ToString(),
                Total = t.Total,
                OrderId = t.OrderId
            });
            return Ok(listResponse);
        }

        [HttpGet("user-transactions/{userId}")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllByUserId(int userId)
        {
            var transactionList = await _getAllByUserTransactionUseCase.Execute(userId);
            var listResponse = transactionList.Select(t => new TransactionDTO
            {
                TransactionId = t.TransactionId,
                CreatedAt = t.CreatedAt,
                Status = t.Status.ToString(),
                Total = t.Total,
                OrderId = t.OrderId
            });
            return Ok(listResponse);
        }
    }
}