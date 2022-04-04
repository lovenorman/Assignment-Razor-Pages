using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    //1. En lista ska visas med kundnummer och personnummer, namn, adress, city
    //  som sökresultat.Sökresultatet ska vara paginerat (50 resultat i taget
    //  och så ska man kunna bläddra till nästa/tidigare sida).
    //2.  Klickar man på en kund ska man komma till kundbilden.


    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomersViewModel> CustomersList = new List<CustomersViewModel>();

        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public int PageNo { get; set; }

        public int TotalPageCount { get; set; }

        public string SearchWord { get; set; }

        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string NationalId { get; set; } 
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
        }

        public void OnGet(string searchWord, string col = "Id", string order = "asc", int pageno = 1)
        {
            PageNo = pageno;
            SearchWord = searchWord;
            SortCol = col;
            SortOrder = order;

            var c = _context.Customers.AsQueryable();

            //SortByName
            if (!string.IsNullOrEmpty(SearchWord))
                c = c.Where(c => c.Givenname.Contains(searchWord)
                                    || c.Surname.Contains(searchWord)
                                    || c.City.Contains(searchWord)
                           );

            //OrderBy
            c = c.OrderBy(col,
                order == "asc" ? ExtensionMethods.QuerySortOrder.Asc :
                    ExtensionMethods.QuerySortOrder.Desc);

            //SearchId = searchId;
            //var i = _context.Customers.AsQueryable();

            //i = i.Where(i => i.Id == searchId);



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

            var pageResult = c.GetPaged(PageNo, 20);
            TotalPageCount = pageResult.PageCount;

            CustomersList = pageResult.Results.Select(c =>
            new CustomersViewModel
            {
                Id = c.Id,
                NationalId = c.NationalId,
                Givenname = c.Givenname,
                Surname = c.Surname,
                Streetaddress = c.Streetaddress,
                City = c.City  
            }).ToList();
        }
    }
}
