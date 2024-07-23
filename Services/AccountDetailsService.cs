using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class AccountDetailsService /*: IAccountDetailsService*/
    {
        //private readonly DataAccessService _dataAccessService;
        //private readonly IMapper _mapper;
        //public AccountDetailsService (DataAccessService dataAccessService, IMapper mapper)
        //{
        //    _dataAccessService = dataAccessService;
        //    _mapper = mapper;
        //}

        //public async Task<List<LoanViewModel>> GetLoansByAccountIdAsync(int accountId)
        //{
        //    return await _dataAccessService.GetDbContext().Loans
        //        .Where(l => l.AccountId == accountId)
        //        .Select(l => new LoanViewModel
        //        {
        //            LoanId = l.LoanId,
        //            Date = l.Date,
        //            Amount = l.Amount,
        //            Duration = l.Duration,
        //            Payments = l.Payments,
        //            Status = l.Status
        //        })
        //        .ToListAsync();
        //}

        //public async Task<List<PermanentOrderViewModel>> GetPermanentOrdersByAccountIdAsync(int accountId)
        //{
        //    return await _dataAccessService.GetDbContext().PermenentOrders
        //        .Where(po => po.AccountId == accountId)
        //        .Select(po => new PermanentOrderViewModel
        //        {
        //            OrderId = po.OrderId,
        //            BankTo = po.BankTo,
        //            AccountTo = po.AccountTo,
        //            Amount = po.Amount,
        //            Symbol = po.Symbol
        //        })
        //        .ToListAsync();
        //}
    }
}
