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
using DataAccessLayer.Models.CountrySystem;
using System.Text.Json;

namespace BatchProcessor
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
            Console.WriteLine("Batch processing started.");

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

			var serviceProvider = new ServiceCollection()
				.AddDbContext<BankAppDataV2Context>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
				.AddScoped<BatchProcessor>()
				.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
			{
                var batchProcessor = scope.ServiceProvider.GetRequiredService<BatchProcessor>();

                var countries = Enum.GetValues(typeof(Country))
                    .Cast<Country>()
                    .Where(c => c != Country.Choose)
                    .ToList();

                foreach (var country in countries)
				{
                    Console.WriteLine($"Processing transactions for country: {(Country)country}");
                    batchProcessor.ProcessTransactionsByCountry(country);
				}

                Console.WriteLine("Batch processing completed.");
            }

		}
        public static class JsonOptions
        {
            public static JsonSerializerOptions Default { get; } = new JsonSerializerOptions
            {
                Converters = { new DateOnlyJsonConverter() }
            };
        }
    }
}
