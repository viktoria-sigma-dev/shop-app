using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApp.src.Application.DTOs.UserDTOs;
using UsersApp.src.Application.UseCases.UserCases;
using UsersApp.src.Domain.Entities;

namespace UsersApp.src.WebApi.Controllers
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
        private readonly GetBalanceHistoryUserUseCase _getBalanceHistoryUserUseCase;
        private readonly GetOrdersUserUseCase _getOrdersUserUseCase;
        public UserController(CreateUserUseCase createUserUseCase, DeleteUserUseCase deleteUserUseCase, GetAllUserUseCase getAllUserUseCase, GetOneUserUseCase getOneUserUseCase, UpdateUserUseCase updateUserUseCase, GetBalanceUserUseCase getBalanceUserUseCase, GetBalanceHistoryUserUseCase getBalanceHistoryUserUseCase, GetOrdersUserUseCase getOrdersUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getAllUserUseCase = getAllUserUseCase;
            _getOneUserUseCase = getOneUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _getBalanceUserUseCase = getBalanceUserUseCase;
            _getBalanceHistoryUserUseCase = getBalanceHistoryUserUseCase;
            _getOrdersUserUseCase = getOrdersUserUseCase;
        }

        [HttpPost()]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserDTO userDto)
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
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var userList = await _getAllUserUseCase.Execute();
            return Ok(userList);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetOne(int userId)
        {
            var user = await _getOneUserUseCase.Execute(userId);
            return Ok(user);
        }
        [HttpPatch("{userId}")]
        public async Task<ActionResult<User>> Update(int userId, [FromBody] UpdateUserDTO userDto)
        {
            var user = await _updateUserUseCase.Execute(userId, userDto);
            return Ok(user);
        }
        [HttpGet("{userId}/balance")]
        public async Task<ActionResult<int>> GetBalance(int userId)
        {
            var balance = await _getBalanceUserUseCase.Execute(userId);
            return Ok(balance);
        }
        [HttpGet("{userId}/balance-history")]
        public async Task<ActionResult<List<Transaction>>> GetBalanceHistory(int userId)
        {
            var history = await _getBalanceHistoryUserUseCase.Execute(userId);
            return Ok(history);
        }
        [HttpGet("{userId}/orders")]
        public async Task<ActionResult<List<Order>>> GetOrders(int userId)
        {
            var orders = await _getOrdersUserUseCase.Execute(userId);
            return Ok(orders);
        }
    }
}