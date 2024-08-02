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
