using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class GetBalanceUserUseCase(IUserRepository userRepository)
    {
        public IUserRepository _userRepository = userRepository;

        public async Task<decimal> Execute(int id)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null) throw new UserNotFoundException(id);

            return user.Balance;
        }
    }
}