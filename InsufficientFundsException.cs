using System;

namespace BusinessLogic.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(int accountId, decimal balance, decimal amount)
            : base($"Balans kifayət deyil. AccountId: {accountId} | Balans: {balance} | Tələb olunan: {amount}")
        {
        }
    }
}