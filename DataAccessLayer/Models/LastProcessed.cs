using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class LastProcessed
    {
        public int LastProcessedId { get; set; }
        public int CustomerId { get; set; }
        public DateTime LastProcessedDate { get; set; }
    }
}
