using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Contract;
using DataAccess.Models;
using DataAccess.Models.Enums;
using DataAccess.Reposi.Contracts;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Exceptions;
namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepos _accountRepos;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepos accountRepos, IMapper mapper)
        {
            _accountRepos = accountRepos;
            _mapper = mapper;
        }

        public AccountDto? GetById(int id)
        {
            var account = _accountRepos.GetAll(
                include: q => q.Include(a => a.Customer),
                predicate: a => a.Id == id
            ).FirstOrDefault();

            return account == null ? null : _mapper.Map<AccountDto>(account);
        }

        public AccountDto? GetByAccountNumber(string accountNumber)
        {
            var account = _accountRepos.GetAll(
                include: q => q.Include(a => a.Customer),
                predicate: a => a.AccountNumber == accountNumber
            ).FirstOrDefault();

            return account == null ? null : _mapper.Map<AccountDto>(account);
        }

        public List<AccountDto> GetByCustomerId(int customerId)
        {
            var accounts = _accountRepos.GetAll(
                include: q => q.Include(a => a.Customer),
                predicate: a => a.CustomerId == customerId
            );
            return _mapper.Map<List<AccountDto>>(accounts);
        }

        public List<AccountDto> GetAccountsAboveBalance(decimal threshold)
        {
            var accounts = _accountRepos.GetAll(
                include: q => q.Include(a => a.Customer),
                predicate: a => a.Balance > threshold && a.Status == AccountStatus.Active
            );
            return _mapper.Map<List<AccountDto>>(accounts);
        }

        public void OpenAccount(CreateAccountDto dto)
        {
            var account = _mapper.Map<Account>(dto);
            account.AccountNumber = "ACC" + DateTime.UtcNow.Ticks.ToString().Substring(0, 13);
            account.Balance = 0;
            account.Status = AccountStatus.Active;
            account.OpenedAt = DateTime.UtcNow;

            _accountRepos.Add(account);
        }

        public void CloseAccount(int accountId)
        {
            var account = _accountRepos.GetById(accountId)
                ?? throw new KeyNotFoundException("Hesab tapılmadı.");

            if (account.Status == AccountStatus.Closed)
                throw new AccountClosedException(accountId);

            if (account.Balance != 0)
                throw new InvalidOperationException("Hesabda qalıq var, bağlamaq olmaz.");

            account.Status = AccountStatus.Closed;
            account.ClosedAt = DateTime.UtcNow;
            _accountRepos.Update(accountId, account);
        }

        public void Deposit(int accountId, decimal amount, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Məbləğ müsbət olmalıdır.");

            var account = _accountRepos.GetById(accountId)
                ?? throw new KeyNotFoundException("Hesab tapılmadı.");

            if (account.Status == AccountStatus.Closed)
                throw new AccountClosedException(accountId);

            account.Balance += amount;
            _accountRepos.Update(accountId, account);
        }

        public void Withdraw(int accountId, decimal amount, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Məbləğ müsbət olmalıdır.");

            var account = _accountRepos.GetById(accountId)
                ?? throw new KeyNotFoundException("Hesab tapılmadı.");

            if (account.Status == AccountStatus.Closed)
                throw new AccountClosedException(accountId);

            if (account.Balance < amount)
                throw new InsufficientFundsException(accountId, account.Balance, amount);

            account.Balance -= amount;
            _accountRepos.Update(accountId, account);
        }

        public void Transfer(int fromAccountId, int toAccountId, decimal amount, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Məbləğ müsbət olmalıdır.");

            var from = _accountRepos.GetById(fromAccountId)
                ?? throw new KeyNotFoundException("Göndərən hesab tapılmadı.");

            var to = _accountRepos.GetById(toAccountId)
                ?? throw new KeyNotFoundException("Alan hesab tapılmadı.");

            if (from.Status == AccountStatus.Closed)
                throw new AccountClosedException(fromAccountId);

            if (to.Status == AccountStatus.Closed)
                throw new AccountClosedException(toAccountId);

            if (from.Balance < amount)
                throw new InsufficientFundsException(fromAccountId, from.Balance, amount);

            from.Balance -= amount;
            to.Balance += amount;

            _accountRepos.Update(fromAccountId, from);
            _accountRepos.Update(toAccountId, to);
        }
    }
}