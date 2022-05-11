using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using NToastNotify;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    [BindProperties]
    public class NewCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public NewCustomerModel(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }

        //[Range(0, 3)]
        public int TelephoneCountryCode { get; set; }

        //[Range(0, 10, ErrorMessage = "9 siffror max")]
        public string Telephone { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        
        public void OnGet()
        {
            Birthday = DateTime.Today;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var newCustomer = new Data.Customer();
                newCustomer.Givenname = Givenname;
                newCustomer.Surname = Surname;
                newCustomer.Streetaddress = Streetaddress;
                newCustomer.City = City;
                newCustomer.Zipcode = Zipcode;
                newCustomer.Country = Country;
                newCustomer.CountryCode = CountryCode;
                newCustomer.NationalId = NationalId;
                newCustomer.TelephoneCountryCode = TelephoneCountryCode;
                newCustomer.Telephone = Telephone;
                newCustomer.EmailAddress = EmailAddress;
                newCustomer.Birthday = Birthday;

                _context.Customers.Add(newCustomer);
                newCustomer.Accounts.Add(new Account { AccountType = "Personal", Created = DateTime.Now, Balance = 0});
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("New customer created!");

                return RedirectToPage("CustomerDetail", new { Id = newCustomer.Id });
                //return RedirectToPage("CustomersList");
            }

            //Visar felen och ritar om sidan
            return Page();
        }
    }
}
