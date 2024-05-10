using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataAccessService _dataAccessService;
        private readonly IMapper _mapper;
        public CustomerService(DataAccessService dbContext, IMapper mapper)
        {
            _dataAccessService = dbContext;
            _mapper = mapper;
        }


        public IEnumerable<Customer> GetCustomers()
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                return dbContext.Customers;
            }
        }

        public int SaveNew(Customer customer)
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
                return customer.CustomerId;
            }
        }

        public void Update()
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                dbContext.SaveChanges();
            }
        }

        public Customer GetCustomer(int customerId)
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                return dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            }
        }
        public PagedResult<CustomerSearchViewModel> ReadCustomers(string sortColumn, string sortOrder, int page)
        {
            var query = _dataAccessService.GetCustomersQuery();
           

            if (sortColumn == "Givenname")
            {
                query = sortOrder == "asc" ? query.OrderBy(c => c.Givenname) : query.OrderByDescending(c => c.Givenname);
            }
            else if (sortColumn == "City")
            {
                query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);
            }
            var customers = _mapper.ProjectTo<CustomerSearchViewModel>(query).AsQueryable();
            var pagedCustomers = customers.GetPaged(page, 50);
            return pagedCustomers;
        }
    }
}
