using AutoMapper;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankProject.Pages.CustomerSystem
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CustomersModel(ICustomerService customerService)
        {
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}
		public PagedResult<CustomerSearchViewModel> Customers { get; set; }
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageCount { get; set; }
        public void OnGet(string sortColumn, string sortOrder, int pageNo)
        {
            try
            {
                SortColumn = sortColumn;
                SortOrder = sortOrder;

                if (pageNo == 0)
                    pageNo = 1;
                CurrentPage = pageNo;

                Customers = _customerService.ReadCustomers(sortColumn, sortOrder, pageNo);
                PageCount = Customers.PageCount;
            }
            catch (Exception ex)
            {
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

        }
    }
}
