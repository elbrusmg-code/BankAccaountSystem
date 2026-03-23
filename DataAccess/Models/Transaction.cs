using DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Description { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public int? RelatedAccountId { get; set; }  // transfer üçün

        // Navigation
        public Account Account { get; set; } = null!;
        public Account? RelatedAccount { get; set; }
    }
}
