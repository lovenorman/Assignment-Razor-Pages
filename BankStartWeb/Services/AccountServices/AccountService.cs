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

        public IAccountService.ErrorCode Withdraw(int accountId, decimal Amount, string Type)
        {
            if(Amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var account = _context.Accounts.First(a => a.Id == accountId);

            if (account.Balance < Amount)
            {
                return IAccountService.ErrorCode.BalanceIsTooLow;
            }

            account.Balance -= Amount;

            var transaction = new Transaction();
            {
                transaction.Type = Type;
                transaction.Operation = "Withdrawal";
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = Amount;
                transaction.NewBalance = account.Balance;
            }

            account.Transactions.Add(transaction);//Adds to the list of transaction for this specific customer->account
            _context.SaveChanges();

            return IAccountService.ErrorCode.Ok;
        }

        public IAccountService.ErrorCode Deposit(int accountId, decimal Amount, string Type)
        {
            if (Amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var account = _context.Accounts.First(a => a.Id == accountId);

            account.Balance += Amount;

            var transaction = new Transaction();
            {
                transaction.Type = Type;
                transaction.Operation = "Deposit";
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = Amount;
                transaction.NewBalance = account.Balance;
            }

            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.Ok;
        }

        public IAccountService.ErrorCode Transfer(int fromAccountId, int toAccountId, decimal Amount, string Type)
        {
            if (Amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var fromAccount = _context.Accounts.First(a => a.Id == fromAccountId);
            var toAccount = _context.Accounts.First(b => b.Id == toAccountId);

        }
    }
}
