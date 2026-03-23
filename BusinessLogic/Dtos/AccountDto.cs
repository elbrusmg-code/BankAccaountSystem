using DataAccess.Models.Enums;
namespace BusinessLogic.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public AccountType AccountType { get; set; }   // string yox enum
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }      // string yox enum
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; } = null!;
    }

    public class CreateAccountDto
    {
        public int CustomerId { get; set; }
        public AccountType AccountType { get; set; }   // string yox enum
    }

    public class UpdateAccountDto
    {
        public AccountStatus Status { get; set; }
    }
}