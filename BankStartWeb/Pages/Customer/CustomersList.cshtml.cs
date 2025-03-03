using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customer
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomersListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomersViewModel> CustomersList = new List<CustomersViewModel>();

        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public int PageNo { get; set; }
        public int TotalPageCount { get; set; }
        public string SearchWord { get; set; }
        public string SearchId { get; set; }

        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string NationalId { get; set; } 
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
        }

        public void OnGet(string searchWord, string searchId, string col = "Id", string order = "asc", int pageno = 1)
        {
            PageNo = pageno;
            SearchWord = searchWord;
            SortCol = col;
            SortOrder = order;
            SearchId = searchId;

            var c = _context.Customers.AsQueryable();

            //Search (name or city)
            if (!string.IsNullOrEmpty(SearchWord))
                c = c.Where(c => c.Givenname.Contains(SearchWord)
                                        || c.Surname.Contains(SearchWord)
                                        || c.City.Contains(SearchWord)
                               );

            //Search by NationalId
            if (!string.IsNullOrEmpty(SearchId))
                c = c.Where(c => c.NationalId.Contains(SearchId)
                            );

            //OrderBy
            c = c.OrderBy(col,
                order == "asc" ? ExtensionMethods.QuerySortOrder.Asc :
                    ExtensionMethods.QuerySortOrder.Desc);

            //Paging
            var pageResult = c.GetPaged(PageNo, 20);
            TotalPageCount = pageResult.PageCount;

            //H�mtar fr�n databas
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
