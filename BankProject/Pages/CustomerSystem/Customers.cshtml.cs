using AutoMapper;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Services;

namespace BankProject.Pages.CustomerSystem
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersModel(ICustomerService customerService)
        {
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}
		public PagedResult<CustomerSearchViewModel> Customers { get; set; }
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageCount { get; set; }
        public string SearchText { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(string sortColumn, string sortOrder, int pageNo, string searchText = null)
        {
            try
            {
                SortColumn = sortColumn;
                SortOrder = sortOrder;

                if (pageNo == 0)
                    pageNo = 1;
                CurrentPage = pageNo;

                SearchText = searchText;

                Customers = _customerService.ReadCustomers(sortColumn, sortOrder, pageNo, searchText);
                PageCount = Customers.PageCount;

                if (!Customers.Results.Any())
                {
                    ErrorMessage = "No customers found";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }
        }
    }
}
