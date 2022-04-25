using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public EditCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        public int TelephoneCountryCode { get; set; }

        [Range(0, 10, ErrorMessage = "9 siffror max")]
        public string Telephone { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }

        public void OnGet(int Id)
        {
            var customer = _context.Customers.First(c => c.Id == Id);
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
                var customer = _context.Customers.First(e => e.Id == id);
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;

                _context.SaveChanges();

                return RedirectToPage("CustomersDetail", new { id = id });
            }

            return Page();
        }
    }
}
