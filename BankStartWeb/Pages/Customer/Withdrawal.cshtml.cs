using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class WithdrawalModel : PageModel
    {
        private ApplicationDbContext _context;
        private IAccountService _accountService;

        public WithdrawalModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [BindProperty]
        [Range(1, 5000)]
        public decimal Amount { get; set; }

        public void OnGet(int Id)
        {

        }

        public IActionResult OnPost(int Id, decimal Amount)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Withdraw(Id, Amount);
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
