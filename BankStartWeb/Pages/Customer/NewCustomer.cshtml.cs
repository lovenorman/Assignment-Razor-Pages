using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customer
{
    [BindProperties]
    public class NewCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewCustomerModel(ApplicationDbContext context)
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
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        
        public void OnGet()
        {
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

                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                return RedirectToPage("CustomersList");
            }

            //Visar felen och ritar om sidan
            return Page();
        }
    }
}
