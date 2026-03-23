using DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; } = 0;
        public AccountStatus Status { get; set; } = AccountStatus.Active;
        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        // Navigation
        public Customer Customer { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
