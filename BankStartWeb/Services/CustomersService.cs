﻿using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public class CustomersService : ICustomersService
    {
        //1.Implementing methods
        //2. Parameters in constructor?

        private readonly ApplicationDbContext _context;
        public CustomersService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public string SearchWord { get; set; }
        public int SearchId { get; set; }

        public string SearchByName(string searchWord)
        {
            throw new NotImplementedException();
        }

        public int SearchById()
        {
            throw new NotImplementedException();
        }

        public string SearchByName()
        {
            throw new NotImplementedException();
        }
    }
}
