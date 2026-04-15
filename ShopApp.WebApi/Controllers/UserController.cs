using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Application.DTOs.TransactionDTOs;
using ShopApp.Application.DTOs.UserDTOs;
using ShopApp.Application.UseCases.UserCases;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(CreateUserUseCase createUserUseCase, DeleteUserUseCase deleteUserUseCase, GetAllUserUseCase getAllUserUseCase, GetOneUserUseCase getOneUserUseCase, UpdateUserUseCase updateUserUseCase, GetBalanceUserUseCase getBalanceUserUseCase, TopUpBalanceUserUseCase topUpBalanceUserUseCase, GetBalanceHistoryUserUseCase getBalanceHistoryUserUseCase, GetOrdersUserUseCase getOrdersUserUseCase) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult<UserResponseDTO>> Create([FromBody] CreateUserDTO userDto)
        {
            var user = await createUserUseCase.Execute(userDto);
            return Ok(user);
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            await deleteUserUseCase.Execute(userId);
            return Ok();
        }
        [HttpGet()]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAll()
        {
            var userList = await getAllUserUseCase.Execute();
            return Ok(userList);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> GetOne(int userId)
        {
            var user = await getOneUserUseCase.Execute(userId);
            return Ok(user);
        }
        [HttpPatch("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> Update(int userId, [FromBody] UpdateUserDTO userDto)
        {
            var user = await updateUserUseCase.Execute(userId, userDto);
            return Ok(user);
        }
        [HttpGet("{userId}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(int userId)
        {
            var balance = await getBalanceUserUseCase.Execute(userId);
            return Ok(balance);
        }
        [HttpPatch("{userId}/balance")]
        public async Task<ActionResult<UserResponseDTO>> TopUpBalance(int userId, [FromBody] TopUpUserBalanceDTO balanceDTO)
        {
            var user = await topUpBalanceUserUseCase.Execute(userId, balanceDTO.Amount);
            return Ok(user);
        }
        [HttpGet("{userId}/balance-history")]
        public async Task<ActionResult<List<TransactionResponseDTO>>> GetBalanceHistory(int userId)
        {
            var history = await getBalanceHistoryUserUseCase.Execute(userId);
            return Ok(history);
        }
        [HttpGet("{userId}/orders")]
        public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders(int userId)
        {
            var orders = await getOrdersUserUseCase.Execute(userId);
            return Ok(orders);
        }
    }
}