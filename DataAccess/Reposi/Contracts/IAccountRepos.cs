using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Reposi.Contracts
{
    public interface IAccountRepos:IRepository<Account>
    {
        Account? GetByAccountNumber(string accountNumber);
        List<Account> GetByCustomerId(int customerId);
        List<Account> GetAccountsAboveBalance(decimal threshold);
    }
}
