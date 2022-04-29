using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    [BindProperties]
    public class CustomerDetailModel : PageModel
    {
        //1. Det ska gå att ta fram en kundbild genom att ange kundnummer.
        //2. Kundbilden ska visa all information om kunden och alla kundens konton.
        //3. Kundbilden ska också visa det totala saldot för kunden genom att
        //   summera saldot på kundens konton.
        //4. Genom attt klicka på ett kontonummer ska man komma vidare till
        //   en kontosida.

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
        public int TotalBalance { get; set; }

        public class AccountDetailViewModel
        {
            public int Id { get; set; }
            public string AccountType { get; set; }
            public decimal Balance { get; set; }
        }

        public void OnGet(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Accounts)
                .First(c => c.Id == id);
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
                Id = c.Id,
                AccountType = c.AccountType,
                Balance = c.Balance,
            }).ToList();

            TotalBalance = (int)customer.Accounts.Sum(x => x.Balance);
            
        }
    }
}
