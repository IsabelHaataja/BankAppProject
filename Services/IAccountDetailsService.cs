using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountDetailsService
    {
         Task<List<PermanentOrderViewModel>> GetPermanentOrdersByAccountIdAsync(int accountId);
         Task<List<LoanViewModel>> GetLoansByAccountIdAsync(int accountId);
    }
}
