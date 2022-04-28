using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class TransferModel : PageModel
    {
        private ApplicationDbContext _context;
        public IAccountService _accountService;
        
        public TransferModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int customerId { get; set; }
        public decimal Amount { get; set; }
        [BindProperty]
        public string FromAccount { get; set; }
        [BindProperty]
        public string ToAccount { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }

        public void OnGet()
        {
            SetAllAccounts(customerId);
        }

        public void SetAllAccounts(int customerId)
        {
            //Hämtar kund och include alla konton hos kunden som matchar id(anger mapp som typ pga 2 "customer" som filnamn
            Data.Customer customer = _context.Customers.Include(e => e.Accounts).FirstOrDefault(e => e.Id == customerId); //Jag vill komma åt de konton som tillhör specifik kund
            //Hämtar alla konton hos kund
            AllAccounts = customer.Accounts.Select(account => new SelectListItem
            {
                Text = account.AccountType + " " + account.Balance,
                Value = account.Id.ToString()
            }).ToList();

            AllAccounts.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Please select an account"
            });
        }

        public IActionResult OnPost(int fromAccountId, int toAccountId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Transfer(fromAccountId, toAccountId, amount);
                if (status == IAccountService.ErrorCode.Ok)
                {
                    return RedirectToPage("AccountDetails", new { id = fromAccountId });
                }
                ModelState.AddModelError("Amount", "Beloppet är fel");

                return Page();
            }

            //Ritar om formuläret med fel
            //SetAllAccounts();

            return Page();
        }
    }
}
