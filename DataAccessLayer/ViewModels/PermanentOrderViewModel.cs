using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
    public class PermanentOrderViewModel
    {
        public int OrderId { get; set; }
        public string BankTo { get; set; } = null!;
        public string AccountTo { get; set; } = null!;
        public decimal? Amount { get; set; }
        public string Symbol { get; set; } = null!;
    }
}
