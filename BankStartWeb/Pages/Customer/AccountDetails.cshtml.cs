using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customer
{
    //1. N�r man klickar p� ett kontonummer i kundbilden ska man komma till en
    //  kontosida som visar kontonummer och saldo samt en lista med transaktioner
    //  i descending order.

    //2. Om det finns fler �n 20 transaktioner ska
    //  JavaScript/AJAX anv�ndas f�r att ladda in ytterligare 20 transaktioner
    //  n�r man trycker p� en knapp l�ngst ned i listan. Trycker man igen laddas
    //  20 till, och s� vidare.

    [Authorize(Roles = "Admin, Cashier")]
    [BindProperties]
    public class AccountDetailsModel : PageModel
    {
        private ApplicationDbContext _context;

        public AccountDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Id { get; set; }
        public string AccountType { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public List<Transactions> TransactionList { get; set; }

        public class Transactions
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
        }

        public void OnGet(int Id)
        {
            var account = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.Id == Id);
            Id = account.Id;
            AccountType = account.AccountType;
            Created = account.Created;  
            Balance = account.Balance;

            TransactionList = account.Transactions.Select(t => new Transactions
            {
                Id = t.Id,
                Type = t.Type,
                Operation = t.Operation,
                Date = t.Date,
                Amount = t.Amount,
            }).ToList();
        }
    }
}
