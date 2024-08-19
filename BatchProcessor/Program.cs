using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication;
using DataAccessLayer.Models.CountrySystem;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Hosting;
using Services;
using AutoMapper;
using DataAccessLayer;

namespace BatchProcessor
{
	public class Program
	{
        public static async Task Main(string[] args)
        {
            var countries = new[] { Country.Sweden, Country.Finland, Country.Norway, Country.Denmark };

            var serviceProvider = new ServiceCollection()
                .AddDbContext<BankAppDataContext>(options =>
                    options.UseSqlServer("YourConnectionStringHere"), ServiceLifetime.Scoped)
                .AddScoped<Func<BankAppDataContext>>(provider =>
                    () => provider.GetService<BankAppDataContext>())
                .AddSingleton<DataAccessService>()
                .AddAutoMapper(typeof(Program))
                .AddTransient<TransactionService>()
                .AddTransient<LastProcessedService>()
                .BuildServiceProvider();

            var transactionService = serviceProvider.GetService<TransactionService>();
            var lastProcessedService = serviceProvider.GetService<LastProcessedService>();

            try
            {
                foreach (var country in countries)
                {
                    Console.WriteLine($"Batch processor started for {country}");
                    var users = await transactionService.GetCustomersByCountryAsync(country);
                    var report = new StringWriter();
                    bool hasSuspiciousTransactions = false;

                    foreach (var user in users)
                    {
                        var lastProcessedDate = await lastProcessedService.GetLastProcessedDateForCustomer(user.CustomerId);
                        var suspiciousTransactions = await transactionService.GetSuspiciousTransactionsForCustomer(user.CustomerId, lastProcessedDate, users.Count == 0);

                        if (suspiciousTransactions.Any())
                        {
                            hasSuspiciousTransactions = true;
                            foreach (var transaction in suspiciousTransactions)
                            {
                                report.WriteLine($"Customer: {user.Givenname} {user.Surname}, Account: {transaction.AccountNavigation.AccountNumber}, Transaction: {transaction.TransactionId}");
                            }
                        }

                        await lastProcessedService.UpdateLastProcessedDateForCustomer(user.CustomerId, DateTime.UtcNow);
                    }

                    var reportPath = $"SuspiciousReport_{country}_{DateTime.UtcNow:yyyyMMdd}.txt";
                    if (hasSuspiciousTransactions)
                    {                         

                        await File.WriteAllTextAsync(reportPath, report.ToString());
                        Console.WriteLine($"Report for {country} saved to {reportPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No suspicious transactions found for {country}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
