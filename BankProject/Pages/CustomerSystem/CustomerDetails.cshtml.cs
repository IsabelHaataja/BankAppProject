using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankProject.Pages.CustomerSystem
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomerDetailsModel : PageModel
    {
		private readonly ICustomerDetails _customerDetails;
		public CustomerDetailsModel(ICustomerDetails customerDetails)
		{
			_customerDetails = customerDetails;
		}
		public CustomerViewModel ChosenCustomer { get; set; }
		[BindProperty(SupportsGet = true)]
		public int CustomerId { get; set; }

		public async Task OnGetAsync()
		{
			ChosenCustomer = await _customerDetails.GetCustomerAsync(CustomerId);
        }
        public string FormatCardNumber(string cardNumber)
        {
            return _customerDetails.FormatCardNumber(cardNumber);
        }
    }
}
