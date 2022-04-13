using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public interface IAccountService
    {
        public int Withdrawal(int Id, int amount);

        public int Deposit(int Id, int amount);

    }
}
