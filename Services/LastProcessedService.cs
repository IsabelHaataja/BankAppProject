using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LastProcessedService : ILastProcessedService
    {
        private readonly BankAppDataContext _bankAppDataContext;
        public LastProcessedService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public async Task<DateTime> GetLastProcessedDateForCustomer(int customerId)
        {
            var lastProcessed = await _bankAppDataContext.LastProcesseds
                .FirstOrDefaultAsync(lp => lp.CustomerId == customerId);
            return lastProcessed?.LastProcessedDate ?? DateTime.MinValue;
        }
        public async Task UpdateLastProcessedDateForCustomer(int customerId, DateTime lastProcessedDate)
        {
            var lastProcessed = await _bankAppDataContext.LastProcesseds
                .FirstOrDefaultAsync(lp => lp.CustomerId == customerId);

            if (lastProcessed == null)
            {
                lastProcessed = new LastProcessed
                {
                    CustomerId = customerId,
                    LastProcessedDate = lastProcessedDate
                };
                _bankAppDataContext.LastProcesseds.Add(lastProcessed);
            }
            else
            {
                lastProcessed.LastProcessedDate = lastProcessedDate;
            }

            await _bankAppDataContext.SaveChangesAsync();
        }
    }
}
