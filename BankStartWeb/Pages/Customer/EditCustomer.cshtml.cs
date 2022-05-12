using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public EditCustomerModel(ApplicationDbContext context, IToastNotification toastNotification)
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

        public void OnGet(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            NationalId = customer.NationalId;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(e => e.Id == id);
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;
                customer.City = City;
                customer.Zipcode = Zipcode;
                customer.Country = Country;
                customer.CountryCode = CountryCode;
                customer.NationalId = NationalId;
                customer.TelephoneCountryCode = TelephoneCountryCode;
                customer.Telephone = Telephone;
                customer.EmailAddress = EmailAddress;
                customer.Birthday = Birthday;

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Customer edited");

                return RedirectToPage("CustomerDetail", new { Id =  id});
            }

            return Page();
        }
    }
}
