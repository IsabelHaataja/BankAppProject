using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication;

namespace BatchProcessor
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
				.Build();

			var serviceProvider = new ServiceCollection()
				.AddDbContext<BankAppDataV2Context>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
				.BuildServiceProvider();

			using (var scope = serviceProvider.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<BankAppDataV2Context>();

				var countries = context.Customers.Select(c => c.CustomerCountry).Distinct();

				foreach (var country in countries)
				{
					var suspiciousTransactions = new List<SuspiciousTransaction>();

					var customers = context.Customers.Where(c => c.CustomerCountry == country);
					foreach (var customer in customers)
					{
						var transactions = context.Transactions.Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customer.CustomerId));
						foreach (var transaction in transactions)
						{
							if (transaction.Amount > 15000 || SuspiciousTransaction.IsSuspicious(transaction, context))
							{
								suspiciousTransactions.Add(new SuspiciousTransaction
								{
									CustomerId = customer.CustomerId,
									AccountId = transaction.AccountId,
									TransactionId = transaction.TransactionId
								});
							}
						}
					}

					var reportPath = Path.Combine("Reports", $"{country}_{DateTime.Now:yyyyMMdd}.txt");
					await File.WriteAllLinesAsync(reportPath, suspiciousTransactions.Select(t => $"{t.CustomerId},{t.AccountId},{t.TransactionId}"));
				}
			}
		}
	}
}
