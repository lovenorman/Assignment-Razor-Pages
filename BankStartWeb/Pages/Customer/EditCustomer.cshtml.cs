using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customer
{
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private  readonly ApplicationDbContext _context;

        public EditCustomerModel(ApplicationDbContext context)
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
            //var customer = _context.Customers
        }
    }
}
