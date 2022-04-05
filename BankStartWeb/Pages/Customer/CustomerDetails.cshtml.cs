using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class CustomerModel : PageModel
    {
        //1. Visar kontonummer och saldo samt en lista med transaktioner i
        //  descending order.

        //2. Om det finns fler än 20 transaktioner ska JavaScript/AJAX
        //  användas för att ladda in ytterligare 20 transaktioner när man
        //  trycker på en knapp längst ned i listan. Trycker man igen laddas 20 till, och så vidare.

        private readonly ApplicationDbContext _context;

        public CustomerModel(ApplicationDbContext context)
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
            public decimal Balance { get; set; }
        }

        public void OnGet(int Id)
        {
            //var customer = _context.customers
            //    .include(c => c.accounts)
            //    .first(c => c.id == id);
            //id = customer.id;
            //nationalid = customer.nationalid;
            //givenname = customer.givenname;
            //surname = customer.surname;
            //streetaddress = customer.streetaddress;
            //city = customer.city;
            //zipcode = customer.zipcode;
            //country = customer.country;
            //countrycode = customer.countrycode;
            //telephonecountrycode = customer.telephonecountrycode;
            //telephone = customer.telephone;
            //emailaddress = customer.emailaddress;
            //birthday = customer.birthday;
            ////accountbalance = customer.accounts.

            //customerdetails = customer.
        }
    }
}
