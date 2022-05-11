using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class TransferModel : PageModel
    {
        private ApplicationDbContext _context;
        public IAccountService _accountService;
        private readonly IToastNotification _toastNotification;

        public TransferModel(ApplicationDbContext context, IAccountService accountService, IToastNotification toastNotification)
        {
            _context = context;
            _accountService = accountService;
            _toastNotification = toastNotification;
        }
        
        public int CustomerId { get; set; }
        public string Name { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public int FromAccount { get; set; }

        [BindProperty]
        public int ToAccount { get; set; }

        public List<SelectListItem> AllAccounts { get; set; }

        public void OnGet(int customerId)
        {
            CustomerId = customerId;
            SetAllAccounts();
            var customer = _context.Customers  //För att visa Name på kund
                .First(c => c.Id == customerId);
            Name = customer.Givenname + " " + customer.Surname;
        }

        public void SetAllAccounts()
        {
            //Hämtar kund och include alla konton hos kunden som matchar id(anger mapp som typ pga 2 "customer" som filnamn
            var customer = _context.Customers
                .Include(e => e.Accounts)
                .FirstOrDefault(e => e.Id == CustomerId); //Jag vill komma åt de konton som tillhör specifik kund
            
            //Hämtar alla konton hos kund och populerar en lista
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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Transfer(FromAccount, ToAccount, Amount);
                if (status == IAccountService.ErrorCode.Ok)
                {
                    _toastNotification.AddSuccessToastMessage("Transfer succesful!");
                    return RedirectToPage("AccountDetails", new { id = FromAccount });
                }
                ModelState.AddModelError("Amount", "Beloppet är fel");

                return Page();
            }

            //Ritar om formuläret med fel
            SetAllAccounts();

            return Page();
        }
    }
}
