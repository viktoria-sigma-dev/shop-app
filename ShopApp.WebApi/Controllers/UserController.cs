using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.DTOs.TransactionDTOs;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Application.UseCases.UserCases;
using ShopApp.Domain.Entities;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase;
        private readonly GetAllUserUseCase _getAllUserUseCase;
        private readonly GetOneUserUseCase _getOneUserUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly GetBalanceUserUseCase _getBalanceUserUseCase;
        private readonly TopUpBalanceUserUseCase _topUpBalanceUserUseCase;
        private readonly GetBalanceHistoryUserUseCase _getBalanceHistoryUserUseCase;
        private readonly GetOrdersUserUseCase _getOrdersUserUseCase;
        public UserController(CreateUserUseCase createUserUseCase, DeleteUserUseCase deleteUserUseCase, GetAllUserUseCase getAllUserUseCase, GetOneUserUseCase getOneUserUseCase, UpdateUserUseCase updateUserUseCase, GetBalanceUserUseCase getBalanceUserUseCase, TopUpBalanceUserUseCase topUpBalanceUserUseCase, GetBalanceHistoryUserUseCase getBalanceHistoryUserUseCase, GetOrdersUserUseCase getOrdersUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getAllUserUseCase = getAllUserUseCase;
            _getOneUserUseCase = getOneUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _getBalanceUserUseCase = getBalanceUserUseCase;
            _topUpBalanceUserUseCase = topUpBalanceUserUseCase;
            _getBalanceHistoryUserUseCase = getBalanceHistoryUserUseCase;
            _getOrdersUserUseCase = getOrdersUserUseCase;
        }

        [HttpPost()]
        public async Task<ActionResult<UserResponseDTO>> Create([FromBody] CreateUserDTO userDto)
        {
            var user = await _createUserUseCase.Execute(userDto);
            return Ok(user);
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            await _deleteUserUseCase.Execute(userId);
            return Ok();
        }
        [HttpGet()]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAll()
        {
            var userList = await _getAllUserUseCase.Execute();
            return Ok(userList);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> GetOne(int userId)
        {
            var user = await _getOneUserUseCase.Execute(userId);
            return Ok(user);
        }
        [HttpPatch("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> Update(int userId, [FromBody] UpdateUserDTO userDto)
        {
            var user = await _updateUserUseCase.Execute(userId, userDto);
            return Ok(user);
        }
        [HttpGet("{userId}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(int userId)
        {
            var balance = await _getBalanceUserUseCase.Execute(userId);
            return Ok(balance);
        }
        [HttpPatch("{userId}/balance")]
        public async Task<ActionResult<UserResponseDTO>> TopUpBalance(int userId, [FromBody] TopUpUserBalanceDTO balanceDTO)
        {
            var user = await _topUpBalanceUserUseCase.Execute(userId, balanceDTO.Amount);
            return Ok(user);
        }
        [HttpGet("{userId}/balance-history")]
        public async Task<ActionResult<List<TransactionResponseDTO>>> GetBalanceHistory(int userId)
        {
            var history = await _getBalanceHistoryUserUseCase.Execute(userId);
            return Ok(history);
        }
        [HttpGet("{userId}/orders")]
        public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders(int userId)
        {
            var orders = await _getOrdersUserUseCase.Execute(userId);
            return Ok(orders);
        }
    }
}