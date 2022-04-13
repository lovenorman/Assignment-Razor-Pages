using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    public class EditCustomer2Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public EditCustomer2Model(ApplicationDbContext context)
        {
            _context = context;
        }

        public string NationalId { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }

        public void OnGet(int Id)
        {
            //Set properties from DB
            var customer = _context.Customers.First(c => c.Id == Id);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
        }

        public IActionResult OnPost(int Id)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.First(e => e.Id == Id);
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;

                _context.SaveChanges();

                return RedirectToPage();
            }
            return Page();
        }
    }
}
