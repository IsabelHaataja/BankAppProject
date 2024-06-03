using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Models;
using Services.Infrastructure;

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
            if (AccountDetails == null)
            {
                TempData["ErrorMessage"] = "Account not found.";
                RedirectToPage("/Error");
            }
        }
        public IActionResult OnGetMoreTransactions(int accountId, int skip)
        {
            var transactions = _accountService.GetMoreTransactions(accountId, skip, 20);
            return new JsonResult(transactions);
        }

        public JsonResult OnPostMakeDeposit([FromBody] DepositViewModel depositInput)
        {
  
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid input" });
            }
            Console.WriteLine($"Data Received: AccountId={depositInput.AccountId}, Amount={depositInput.Amount}, Comment={depositInput.Comment}");
            try
            {
                var errorCode = _accountService.Deposit(depositInput.AccountId, depositInput.Amount, depositInput.Comment);
                if (errorCode != ErrorCode.OK)
                {
                    return new JsonResult(new { success = false, message = $"Deposit failed: {errorCode}" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Internal server error: {ex.Message}" });
            }

            return new JsonResult(new { success = true, message = "Deposit successful!" });
        }
    }
}
