using DataAccessLayer.Models.CountrySystem;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchProcessor
{
    public class Functions
    {
        private readonly BatchProcessor _batchProcessor;

        public Functions(BatchProcessor batchProcessor)
        {
            _batchProcessor = batchProcessor;
        }
    }
}
