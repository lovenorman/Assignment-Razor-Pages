using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customer
{
    //1. När man klickar på ett kontonummer i kundbilden ska man komma till en
    //  kontosida som visar kontonummer och saldo samt en lista med transaktioner
    //  i descending order.

    //2. Om det finns fler än 20 transaktioner ska
    //  JavaScript/AJAX användas för att ladda in ytterligare 20 transaktioner
    //  när man trycker på en knapp längst ned i listan. Trycker man igen laddas
    //  20 till, och så vidare.

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
        public int TotalBalance { get; set; }

        public class Transactions
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public string Date { get; set; }
            public decimal Amount { get; set; }
        }

        public void OnGet(int id)
        {
            var account = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.Id == id);
            Id = account.Id;
            AccountType = account.AccountType;
            Created = account.Created;  
            Balance = account.Balance;

            var customer = _context.Customers
                .Include(c => c.Accounts)
                .First(c => c.Id == id);

            TotalBalance = (int)customer.Accounts.Sum(x => x.Balance);
        }

        public IActionResult OnGetFetchMore(int id, int pageNumber)
        {
            var query = _context.Accounts.Where(a => a.Id == id)
                .SelectMany(a => a.Transactions);

            var pageResult = query.GetPaged(pageNumber, 5);

            var list = pageResult.Results.Select(i => new Transactions
            {
                Id = i.Id,
                Type = i.Type,
                Operation = i.Operation,
                Date = i.Date.ToString("yy-M-d H:m"),
                Amount = i.Amount,
            }).ToList();

            bool lastPage = pageNumber == pageResult.PageCount;

            return new JsonResult(new {items = list, lastPage = lastPage});
        }
    }
}
