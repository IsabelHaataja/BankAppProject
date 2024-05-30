using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
    public class DepositViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
