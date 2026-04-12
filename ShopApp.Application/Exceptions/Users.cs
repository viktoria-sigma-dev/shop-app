namespace ShopApp.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id)
            : base($"User with id '{id}' was not found.")
        {
        }

        public UserNotFoundException(string email)
            : base($"User with email '{email}' was not found.")
        {
        }
    }

    public class DuplicatedUserEmailException : Exception
    {
        public DuplicatedUserEmailException(string email)
            : base($"User with such email {email} already exists!")
        {
        }
    }
}