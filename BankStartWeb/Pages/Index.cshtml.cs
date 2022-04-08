using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public int TotalAmountOfAccounts { get; set; }

        public int TotalAmountOfCustomers { get; set; }

        public int TotalBalance { get; set; }

        public void OnGet()
        {
            TotalAmountOfAccounts = _context.Accounts.Count();
            TotalAmountOfCustomers = _context.Customers.Count();
            TotalBalance = (int)_context.Accounts.Sum(x => x.Balance);
        }
    }
}