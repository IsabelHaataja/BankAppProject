using DataAccessLayer.Models.CountrySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
    public class CountryStatisticsViewModel
    {
        //public int NumberOfCustomersSweden { get; set; }
        //public int NumberOfCustomersFinland { get; set; }
        //public int NumberOfCustomersDenmark { get; set; }
        //public int NumberOfCustomersNorway { get; set; }
        //public int NumberOfAccountsSweden { get; set; }
        //public int NumberOfAccountsFinland { get; set; }
        //public int NumberOfAccountsDenmark { get; set; }
        //public int NumberOfAccountsNorway { get; set; }
        //public decimal TotalBalanceSweden { get; set; }
        //public decimal TotalBalanceFinland { get; set; }
        //public decimal TotalBalanceDenmark { get; set; }
        //public decimal TotalBalanceNorway { get; set; }
        public Dictionary<Country, int> NumberOfCustomers { get; set; }
        public Dictionary<Country, int> NumberOfAccounts { get; set; }
        public Dictionary<Country, decimal> TotalBalance { get; set; }
        public CountryStatisticsViewModel() 
        {
            NumberOfCustomers = new Dictionary<Country, int>();
            NumberOfAccounts = new Dictionary<Country, int>();
            TotalBalance = new Dictionary<Country, decimal>();
        }

    }
}
