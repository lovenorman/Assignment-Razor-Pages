using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public interface IAccountService
    {
        public enum ErrorCode
        {
            Ok,
            BalanceIsTooLow,
            AmountIsNegative,
        }

        ErrorCode Withdraw(int Id, decimal Amount, string Type);

        ErrorCode Deposit(int Id, decimal Amount, string Type);

        ErrorCode Transfer(int Id, decimal Amount, string Type);
    }
}
