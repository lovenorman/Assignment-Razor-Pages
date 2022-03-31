using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public void OnGet(string col="id", string order="asc")
        {
            var c = _context.Customers.AsQueryable();
            
            if(col ==  "id")
            {
                if (order == "asc")
                    c = c.OrderBy(ord => ord.Id);
                else
                    c = c.OrderByDescending(ord => ord.Id);
            }
            else if(col == "givenName")
            {
                if (order == "asc")
                    c = c.OrderBy(ord => ord.Givenname);
                else
                    c = c.OrderByDescending(ord => ord.Givenname);
            }
            else if (col == "surName")
            {
                if (order == "asc")
                    c = c.OrderBy(ord => ord.Surname);
                else
                    c = c.OrderByDescending(ord => ord.Surname);
            }
            else if (col == "streetAddress")
            {
                if (order == "asc")
                    c = c.OrderBy(ord => ord.Streetaddress);
                else
                    c = c.OrderByDescending(ord => ord.Streetaddress);
            }

            CustomersList = c.Take(50).Select(c =>
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
