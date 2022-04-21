using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    public class TransferModel : PageModel
    {
        private ApplicationDbContext _context;

        public TransferModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [Range(1, 5000)]
        public decimal Amount { get; set; }
        public string Type { get; set; }

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
            AllAccounts = _context.Accounts.Select(account => new SelectListItem
            {
                Value = account.AccountType.ToString()
            }).ToList();
            AllAccounts.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Please select an account"
            });


        }
    }
}
