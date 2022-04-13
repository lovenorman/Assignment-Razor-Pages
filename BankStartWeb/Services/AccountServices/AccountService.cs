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
        public int Id { get; set; }
        public int Amount { get; set; }

        public int Deposit(int Id, int amount)
        {
            throw new NotImplementedException();
        }

        public int Withdrawal(int Id, int amount)
        {
            //if (_context.Accounts.Balance < amount)
            //{
            //    ModelState.AddModelError(nameof(amount), "To big amount");
            //    return Page();
            //}
            //return Page();
            throw new NotImplementedException();
        }
    }
}
