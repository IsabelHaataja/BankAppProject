using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Models.CountrySystem;
using NuGet.DependencyResolver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Services;

namespace BatchProcessor
{
    public class BatchProcessor
    {
        private readonly TransactionService transactionService;
        private readonly LastProcessedService lastProcessedService;

        public void ProcessTransactions()
        {

        }
    }
}
