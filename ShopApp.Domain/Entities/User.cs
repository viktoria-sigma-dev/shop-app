
using ShopApp.Domain.Exceptions;

namespace ShopApp.Domain.Entities
{
    public record CreateUserDto
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public string? DateOfBirth { get; init; }
    }
    public class User
    {
        public int UserId { get; init; }
        public User(string firstName, string email, int balance)
        {
            this.FirstName = firstName;
            this.Email = email;
            this.Balance = balance;

        }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? DateOfBirth { get; private set; }
        public decimal Balance { get; private set; } = 0m;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public ICollection<Order>? Orders { get; private set; } = new List<Order>();

        private User() { }
        public User(CreateUserDto dto)
        {
            SetFirstName(dto.FirstName);
            SetLastName(dto.LastName);
            SetEmail(dto.Email);
            SetDOB(dto.DateOfBirth);
            SetBalance(0);
        }
        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidUserValueException("firstName");

            FirstName = firstName.Trim();
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidUserValueException("lastName");

            LastName = lastName.Trim();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidUserValueException("email");

            Email = email.Trim();
        }
        public void SetDOB(string? dob)
        {
            DateOfBirth = dob?.Trim();
        }
        public void SetBalance(int balance)
        {
            if (balance < 0)
                throw new InvalidUserBalanceException();

            Balance = balance;
        }

        public void SetDateOfBirth(string dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(dateOfBirth))
                throw new InvalidUserValueException("dateOfBirth");
            DateOfBirth = dateOfBirth.Trim();
        }
        public void IncreaseBalance(decimal amount)
        {
            if (decimal.IsNegative(amount))
                throw new InvalidUserBalanceException();
            Balance += amount;
        }
        public void DecreaseBalance(decimal amount)
        {
            if (decimal.IsNegative(amount)) throw new InvalidUserBalanceException();
            if (Balance < amount) throw new InsufficientUserBalanceException(Balance);
            Balance -= amount;
        }
    }
}