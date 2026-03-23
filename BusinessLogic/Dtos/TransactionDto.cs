using DataAccess.Models.Enums;

namespace BusinessLogic.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public TransactionType Type { get; set; }      // string yox enum
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Description { get; set; }
        public DateTime OccurredAt { get; set; }
        public int? RelatedAccountId { get; set; }
    }

    public class CreateTransactionDto
    {
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }      // string yox enum
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int? RelatedAccountId { get; set; }
    }
}