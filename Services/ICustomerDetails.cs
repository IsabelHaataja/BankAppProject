using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public interface ICustomerDetails
	{
		Task<CustomerViewModel> GetCustomerAsync(int customerId);
		string FormatCardNumber(string cardNumber);
    }
}
