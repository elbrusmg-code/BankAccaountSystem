using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Customer
    {
       
            public int Id { get; set; }
            public string FullName { get; set; } = null!;
            public string NationalId { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public DateTime DateOfBirth { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public bool IsDeleted { get; set; } = false;

            // Navigation
            public ICollection<Account> Accounts { get; set; } = new List<Account>();
        
    }
}
