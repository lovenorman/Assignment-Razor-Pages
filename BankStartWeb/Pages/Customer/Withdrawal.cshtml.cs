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

        [Range(1, 5000)]
        public int Amount { get; set; }

        public void OnGet(int Id)
        {
            //Amount =
        }

        public IActionResult OnPost(int Id)
        {
            if(ModelState.IsValid)
            {
                var status = _accountService.Withdrawal(Id, Amount);

            }
        }
    }
}
