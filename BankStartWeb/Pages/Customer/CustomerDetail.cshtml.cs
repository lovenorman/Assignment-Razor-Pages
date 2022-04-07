using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customer
{
    public class CustomerDetailModel : PageModel
    {
        //1. Visar kontonummer och saldo samt en lista med transaktioner i
        //  descending order.

        //2. Om det finns fler än 20 transaktioner ska JavaScript/AJAX
        //  användas för att ladda in ytterligare 20 transaktioner när man
        //  trycker på en knapp längst ned i listan. Trycker man igen laddas 20 till, och så vidare.

        private readonly ApplicationDbContext _context;

        public CustomerDetailModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public List<AccountDetailViewModel> AccountRows { get; set; }

        public class AccountDetailViewModel
        {
            public string AccountType { get; set; }
            public decimal Balance { get; set; }
        }

        public void OnGet(int Id)
        {
            var customer = _context.Customers
                .Include(c => c.Accounts)
                .First(c => c.Id == Id);
            Id = customer.Id;
            NationalId = customer.NationalId;
            Givenname = customer.Givenname; 
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress; 
            City = customer.City;
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;

            AccountRows = customer.Accounts.Select(c => new AccountDetailViewModel
            {
                AccountType = c.AccountType,
                Balance = c.Balance,
            }).ToList();
        }
    }
}
