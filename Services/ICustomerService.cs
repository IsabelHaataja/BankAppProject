using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;

namespace Services
{
    public interface ICustomerService
    {
        PagedResult<CustomerSearchViewModel> ReadCustomers(string sortColumn, string sortOrder, int page, string searchText = null);
        public IEnumerable<Customer> GetCustomers();
        int SaveNew(Customer customer);
        void Update();
        Customer GetCustomer(int customerId);
    }
}
