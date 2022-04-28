using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAccountService.ErrorCode Withdraw(int accountId, decimal amount)
        {
            if(amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var account = _context.Accounts.First(a => a.Id == accountId);

            if (account.Balance < amount)
            {
                return IAccountService.ErrorCode.BalanceIsTooLow;
            }

            account.Balance -= amount;

            var transaction = new Transaction();
            {
                transaction.Type = "Debit";
                transaction.Operation = "Withdrawal";
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = amount;
                transaction.NewBalance = account.Balance;
            }

            account.Transactions.Add(transaction);//Adds to the list of transaction for this specific customer->account
            _context.SaveChanges();

            return IAccountService.ErrorCode.Ok;
        }

        public IAccountService.ErrorCode Deposit(int accountId, decimal amount)
        {
            if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var account = _context.Accounts.First(a => a.Id == accountId);

            account.Balance += amount;

            var transaction = new Transaction();
            {
                transaction.Type = "Debit";
                transaction.Operation = "Deposit";
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = amount;
                transaction.NewBalance = account.Balance;
            }

            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.Ok;
        }

        public IAccountService.ErrorCode Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            //Withdrawal from account:
            var account1 = _context.Accounts.First(a => a.Id == fromAccountId);
            if (account1.Balance < amount)
            {
                return IAccountService.ErrorCode.BalanceIsTooLow;
            }

            account1.Balance -= amount;

            var transaction = new Transaction();
            {
                transaction.Type = "Debit";
                transaction.Operation = "Withdrawal";
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = amount;
                transaction.NewBalance = account1.Balance;
            }

            account1.Transactions.Add(transaction);

            //Deposit from account:
            var account2 = _context.Accounts.First(a => a.Id == toAccountId);

            account2.Balance += amount;

            var transaction2 = new Transaction();
            {
                transaction2.Type = "Debit";
                transaction2.Operation = "Deposit";
                transaction2.Date = DateTime.UtcNow;
                transaction2.Amount = amount;
                transaction2.NewBalance = account2.Balance;
            }

            account2.Transactions.Add(transaction2);

            _context.SaveChanges();

            return IAccountService.ErrorCode.Ok;
        }
    }
}
