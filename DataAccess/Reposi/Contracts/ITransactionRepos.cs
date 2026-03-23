using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
namespace DataAccess.Reposi.Contracts
{
    public interface ITransactionRepos :IRepository<Transaction>
    {
        List<Transaction> GetByAccountId(int accountId);
        List<Transaction> GetLast30DaysDeposits();
        List<Transaction> GetInactiveAccounts();
    }
}
