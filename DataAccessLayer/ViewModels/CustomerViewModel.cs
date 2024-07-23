using DataAccessLayer.Models.CountrySystem;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
	public class CustomerViewModel
	{
		public int CustomerId { get; set; }
        public int CustomerNumber { get; set; }

        //public Gender CustomerGender { get; set; }

		public string Givenname { get; set; } = null!;

		public string Surname { get; set; } = null!;

		public string Streetaddress { get; set; } = null!;

		public string City { get; set; } = null!;

		public string Zipcode { get; set; } = null!;

		//public Country CustomerCountry { get; set; }

		public DateOnly? Birthday { get; set; }

		public string? NationalId { get; set; }

		public string? Telephonecountrycode { get; set; }

		public string? Telephonenumber { get; set; }

		public string? Emailaddress { get; set; }
		//public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();
		public List<CustomerViewModel> Customers { get; set; }
		public List<AccountViewModel> Accounts { get; set; }
		public List<CardViewModel> Cards { get; set; }
	}
}
