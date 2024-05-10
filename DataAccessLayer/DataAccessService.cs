using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataAccessService
    {
        private readonly Func<BankAppDataV2Context> _contextFactory;
        private readonly BankAppDataV2Context _context;
        public DataAccessService (Func<BankAppDataV2Context> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory();
        }
        public IQueryable<Customer> GetCustomersQuery()
        {
            return _context.Customers.AsQueryable();
        }
        public BankAppDataV2Context GetDbContext()
        {
            return _contextFactory();
        }
    }
}
