using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    public class DepositModel : PageModel
    {
        
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _context;
        
        public DepositModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [BindProperty]
        [Required(ErrorMessage= "Please enter an amount")]
        [Range(1,5000)]
        public int Amount { get; set; }

        public int Balance { get; set; }

        public void OnGet(int Amount)
        {
            
        }

        public IActionResult OnPost()
        {
            //if(ModelState.IsValid)
            //{
            //    Deposit();
            //}
            return Page();
            
        }

    }
}
