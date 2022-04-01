using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public interface ICustomersService
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string SearchByName(string searchWord);
        {
            SearchWord = searchWord;
        }
        public int SearchById();
    }
}
