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

namespace BatchProcessor
{
    public class BatchProcessor
    {
        private readonly BankAppDataV2Context _context;
        private readonly Dictionary<string, DateOnly> _lastProcessed;
        private const string LastProcessedFilePath = "lastProcessed.json";
        private const string ReportsDirectory = "../../../Transaction_Reports";

        public BatchProcessor(BankAppDataV2Context context)
        {
            _context = context;
            _lastProcessed = LoadLastProcessedState();
        }
        private Dictionary<string, DateOnly> LoadLastProcessedState()
        {
            if (File.Exists(LastProcessedFilePath))
            {
                var json = File.ReadAllText(LastProcessedFilePath);
                var options = new JsonSerializerOptions { Converters = { new DateOnlyJsonConverter() } };
                return JsonSerializer.Deserialize<Dictionary<string, DateOnly>>(json, options) ?? new Dictionary<string, DateOnly>();
            }
            return new Dictionary<string, DateOnly>();
        }
        private void SaveLastProcessedState()
        {
            var options = new JsonSerializerOptions { Converters = { new DateOnlyJsonConverter() } };
            var json = JsonSerializer.Serialize(_lastProcessed, options);
            File.WriteAllText(LastProcessedFilePath, json);
        }
        public void ProcessTransactionsByCountry(Country country)
        {
            try
            {
                Console.WriteLine($"Starting processing for country: {country}");
                var lastProcessedDate = GetLastProcessedDate(country.GetCountryCode());
                var suspiciousTransactions = new List<SuspiciousTransaction>();

                var transactions = _context.Transactions
                    .Include(t => t.AccountNavigation)
                    .ThenInclude(a => a.Dispositions)
                    .Where(t => t.AccountNavigation.Dispositions.Any(d => d.Customer.CustomerCountry == (int)country) && t.Date > lastProcessedDate)
                    .ToList();

                foreach (var transaction in transactions)
                {
                    if (SuspiciousTransaction.IsSuspicious(transaction, _context))
                    {
                        Console.WriteLine($"Suspicious transaction found: TransactionId = {transaction.TransactionId}, Amount = {transaction.Amount}");
                        suspiciousTransactions.Add(new SuspiciousTransaction
                        {
                            CustomerId = transaction.AccountNavigation.Dispositions.First().CustomerId, // Assuming there's at least one disposition per account
                            AccountId = transaction.AccountId,
                            TransactionId = transaction.TransactionId,
                            Amount = transaction.Amount,
                            Date = transaction.Date
                        });
                    }
                }

                if (transactions.Any())
                {
                    var mostRecentTransactionDate = transactions.Max(t => t.Date);
                    UpdateLastProcessedDate(country.GetCountryCode(), mostRecentTransactionDate);
                }

                Console.WriteLine($"Completed processing for country: {country}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during processing for country {country}: {ex.Message}");
            }
        }
        private DateOnly GetLastProcessedDate(string country)
        {
            return _lastProcessed.ContainsKey(country) ? _lastProcessed[country] : DateOnly.MinValue;
        }

        private void UpdateLastProcessedDate(string country, DateOnly date)
        {
            _lastProcessed[country] = date;
            SaveLastProcessedState();
        }
        private void GenerateReport(string countryCode, List<SuspiciousTransaction> suspiciousTransactions)
        {
            var reportFileName = $"SuspiciousTransactions_{countryCode}_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt";
            var reportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Transaction_Reports");
            Directory.CreateDirectory(reportDirectory);
            var reportFilePath = Path.Combine(reportDirectory, reportFileName);

            using (var writer = new StreamWriter(reportFilePath))
            {
                writer.WriteLine("CustomerId AccountId TransactionId Amount Date");

                foreach (var transaction in suspiciousTransactions)
                {
                    writer.WriteLine($"{transaction.CustomerId} - {transaction.AccountId} - {transaction.TransactionId} - {transaction.Amount} - {transaction.Date}");
                }
            }

            Console.WriteLine($"Report generated: {reportFilePath}");
        }
    }
}
