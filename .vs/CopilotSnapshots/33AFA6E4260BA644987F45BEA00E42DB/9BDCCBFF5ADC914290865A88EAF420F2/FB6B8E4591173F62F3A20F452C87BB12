using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Contract;
using DataAccess.Reposi.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepos _transactionRepos;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepos transactionRepos, IMapper mapper)
        {
            _transactionRepos = transactionRepos;
            _mapper = mapper;
        }

        public List<TransactionDto> GetByAccountId(int accountId)
        {
            var transactions = _transactionRepos.GetAll(
                include: q => q.Include(t => t.Account),
                predicate: t => t.AccountId == accountId
            );
            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public List<TransactionDto> GetLast30DaysDeposits()
        {
            var transactions = _transactionRepos.GetLast30DaysDeposits();
            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public List<TransactionDto> GetInactiveAccounts()
        {
            var transactions = _transactionRepos.GetInactiveAccounts();
            return _mapper.Map<List<TransactionDto>>(transactions);
        }
    }
}