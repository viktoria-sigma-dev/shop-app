using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.UserCases
{
    public class DeleteUserUseCase(IUserRepository userRepository)
    {
        public async Task<bool> Execute(int id)
        {
            var deleted = await userRepository.DeleteAsync(id);
            if (!deleted) throw new UserNotFoundException(id);
            return true;
        }
    }
}