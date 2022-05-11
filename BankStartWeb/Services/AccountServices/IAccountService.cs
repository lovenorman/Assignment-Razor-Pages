using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public interface IAccountService
    {
        public enum ErrorCode
        {
            Ok,
            BalanceIsTooLow,
            AmountIsTooLow,
        }

        ErrorCode Withdraw(int Id, decimal Amount);

        ErrorCode Deposit(int Id, decimal Amount);

        ErrorCode Transfer(int fromAccountId, int toAccountId, decimal amount);
    }
}
