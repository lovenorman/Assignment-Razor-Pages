using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class DepositModel : PageModel
    {
        private ApplicationDbContext _context;
        private IAccountService _accountService;

        public DepositModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [Range(1, 5000)]
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }

        public void OnGet(int id)
        {
            var account = _context.Accounts
                .First(a => a.Id == id);
            id = account.Id;

            AccountType = account.AccountType;
        }

        public IActionResult OnPost(int Id, decimal Amount, string Type)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Deposit(Id, Amount, Type);
                if (status == IAccountService.ErrorCode.Ok)
                {
                    return RedirectToPage("AccountDetails", new { id = Id });
                }
                ModelState.AddModelError("Amount", "Beloppet är fel");
            }

            return Page();
        }
    }
}
