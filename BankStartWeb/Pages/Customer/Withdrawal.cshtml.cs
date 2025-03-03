using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class WithdrawalModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IToastNotification _toastNotification;

        public WithdrawalModel(ApplicationDbContext context, IAccountService accountService, IToastNotification toastNotification)
        {
            _context = context;
            _accountService = accountService;
            _toastNotification = toastNotification;
        }

        [BindProperty]
        [Range(1, 5000)]
        public decimal Amount { get; set; }
        public string AccountType { get; set; }

        public void OnGet(int id)
        {
            var account = _context.Accounts
                .First(a => a.Id == id);
            id = account.Id;

            AccountType = account.AccountType;
        }

        public IActionResult OnPost(int id, decimal amount)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Withdraw(id, amount);
                if (status == IAccountService.ErrorCode.Ok)
                {
                    _toastNotification.AddSuccessToastMessage("Withdrawal succesful!");
                    return RedirectToPage("AccountDetails", new { Id = id });
                }
                ModelState.AddModelError("Amount", "Beloppet �r fel");
            }

            return Page();
        }
    }
}
