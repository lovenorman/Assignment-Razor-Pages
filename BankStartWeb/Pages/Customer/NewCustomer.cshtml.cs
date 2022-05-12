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
        [MaxLength(30)]
        public string Givenname { get; set; }

        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(30)]
        public string Streetaddress { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(5)]
        public string Zipcode { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }

        [MaxLength(2, ErrorMessage = "2 letters only")]
        public string CountryCode { get; set; }

        [MaxLength(13)]
        public string NationalId { get; set; }

        [Range(11, 99)]
        public int TelephoneCountryCode { get; set; }

        [MaxLength(20)]
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
            }

            return Page();
        }
    }
}
