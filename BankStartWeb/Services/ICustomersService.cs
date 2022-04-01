using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public interface ICustomersService
    {
        public string SearchByName();
        
        public int SearchById();
        
    }
}
