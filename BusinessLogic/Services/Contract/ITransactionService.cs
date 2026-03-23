using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Contract
{
    public interface ITransactionService
    {
        List<TransactionDto> GetByAccountId(int accountId);
        List<TransactionDto> GetLast30DaysDeposits();
        List<TransactionDto> GetInactiveAccounts();
    }
}