using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomersService _customersService;

        public CustomersModel(ICustomersService customersService)
        {
            _customersService = customersService;   
        }
       
        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
        }

        public List<CustomersViewModel> CustomersList = new List<CustomersViewModel>();

        public void OnGet(int searchId, string searchWord, string col="id", string order="asc")
        {
            //1. Kalla på metoden SearchByName via _customersService


            //SearchId = searchId;
            //var i = _context.Customers.AsQueryable();

            //i = i.Where(i => i.Id == searchId);

            var c = _customersService;
            
            //if(col ==  "id")
            //{
            //    if (order == "asc")
            //        c = c.OrderBy(ord => ord.Id);
            //    else
            //        c = c.OrderByDescending(ord => ord.Id);
            //}
            //else if(col == "givenName")
            //{
            //    if (order == "asc")
            //        c = c.OrderBy(ord => ord.Givenname);
            //    else
            //        c = c.OrderByDescending(ord => ord.Givenname);
            //}
            //else if (col == "surName")
            //{
            //    if (order == "asc")
            //        c = c.OrderBy(ord => ord.Surname);
            //    else
            //        c = c.OrderByDescending(ord => ord.Surname);
            //}
            //else if (col == "streetAddress")
            //{
            //    if (order == "asc")
            //        c = c.OrderBy(ord => ord.Streetaddress);
            //    else
            //        c = c.OrderByDescending(ord => ord.Streetaddress);
            //}

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
