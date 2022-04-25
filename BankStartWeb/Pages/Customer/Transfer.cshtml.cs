using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Range(1, 5000)]
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }

        public void OnGet(int Id)
        {
            SetAllLists();
        }

        private void SetAllLists()
        {
            SetAllAccounts();
        }

        public void SetAllAccounts()
        {
            AllAccounts = _context //Jag vill komma åt de konyton som tillhör specifik kund
                .Accounts.Select(account => new SelectListItem
            {
                Text = account.AccountType,
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
            SetAllLists();

            return Page();
        }
    }
}
