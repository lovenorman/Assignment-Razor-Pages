using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
        }

        public List<CustomersViewModel> CustomersList = new List<CustomersViewModel>();

        public void OnGet()
        {
            CustomersList = _context.Customers.Take(50).Select(c =>
            new CustomersViewModel
            {
                Id = c.Id,
                Givenname = c.Givenname,
                Surname = c.Surname,
                Streetaddress = c.Streetaddress
            }).ToList();
        }
    }
}
