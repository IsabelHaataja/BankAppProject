using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services
{
    public interface ICustomerService
    {
        PagedResult<Customer> ReadCustomers(string sortColumn, string sortOrder, int page);
        public IEnumerable<Customer> GetCustomers();
        //public ApplicationDbContext GetDbContext();
        int SaveNew(Customer customer);
        void Update();
        Customer GetCustomer(int customerId);
    }
}
