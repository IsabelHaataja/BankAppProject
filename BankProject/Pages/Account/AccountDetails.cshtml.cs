using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Models;

namespace BankProject.Pages.Account
{
    public class AccountDetailsModel : PageModel
    {
        private readonly IAccountService _accountService;
        public AccountDetailsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public AccountDetailsViewModel AccountDetails { get; set; }

        public void OnGet(int accountId)
        {
            AccountDetails = _accountService.GetAccountDetails(accountId);
        }
        public IActionResult OnGetMoreTransactions(int accountId, int skip)
        {
            var transactions = _accountService.GetMoreTransactions(accountId, skip, 20);
            return new JsonResult(transactions);
        }
    }
}
