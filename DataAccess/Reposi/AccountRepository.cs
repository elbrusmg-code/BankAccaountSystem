using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Models.Enums;
using DataAccess.Reposi.Contracts;

namespace DataAccess.Reposi;

public class AccountRepository : EfCoreRepository<Account>, IAccountRepos
{
    public AccountRepository(BankContext context) : base(context) { }

    public Account? GetByAccountNumber(string accountNumber)
        => AppDbContext.Accounts
            .FirstOrDefault(a => a.AccountNumber == accountNumber);

    public List<Account> GetByCustomerId(int customerId)
        => AppDbContext.Accounts
            .Where(a => a.CustomerId == customerId)
            .ToList();

    public List<Account> GetAccountsAboveBalance(decimal threshold)
        => AppDbContext.Accounts
            .Where(a => a.Balance > threshold && a.Status == AccountStatus.Active)
            .ToList();
}