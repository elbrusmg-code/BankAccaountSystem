using System;

namespace BusinessLogic.Exceptions
{
    public class AccountClosedException : Exception
    {
        public AccountClosedException(int accountId)
            : base($"Hesab bağlıdır. AccountId: {accountId}")
        {
        }

        public AccountClosedException(string accountNumber)
            : base($"Hesab bağlıdır. AccountNumber: {accountNumber}")
        {
        }
    }
}