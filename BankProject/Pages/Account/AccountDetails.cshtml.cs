using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Models;
using Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace BankProject.Pages.Account
{
    [Authorize(Roles = "Admin, Cashier")]
    public class AccountDetailsModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IAccountDetailsService _accountDetailsService;
        public AccountDetailsModel(IAccountService accountService, IAccountDetailsService accountDetailsService)
        {
            _accountService = accountService;
            _accountDetailsService = accountDetailsService;
        }
        public AccountDetailsViewModel AccountDetails { get; set; }
        public List<LoanViewModel> Loans { get; set; }
        public List<PermanentOrderViewModel> PermanentOrders { get; set; }
        public async Task<IActionResult> OnGetAsync(int accountId)
        {
            AccountDetails = _accountService.GetAccountDetails(accountId);
            if (AccountDetails == null)
            {
                TempData["ErrorMessage"] = "Account not found.";
                RedirectToPage("/Error");
            }

            Loans = await _accountDetailsService.GetLoansByAccountIdAsync(accountId);
            PermanentOrders = await _accountDetailsService.GetPermanentOrdersByAccountIdAsync(accountId);

            return Page();
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
        public JsonResult OnPostMakeWithdraw([FromBody] WithdrawViewModel withdrawInput)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid input" });
            }
            Console.WriteLine($"Data Received: AccountId={withdrawInput.AccountId}, Amount={withdrawInput.Amount}");
            try
            {
                var errorCode = _accountService.Withdraw(withdrawInput.AccountId, withdrawInput.Amount);
                if (errorCode == ErrorCode.BalanceTooLow)
                {
                    return new JsonResult(new { success = false, message = "Withdrawal failed: Balance too low." });
                }
                else if (errorCode != ErrorCode.OK)
                {
                    return new JsonResult(new { success = false, message = $"Withdrawal failed: {errorCode}" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Internal server error: {ex.Message}" });
            }

            return new JsonResult(new { success = true, message = "Withdrawal successful!" });
        }
        public JsonResult OnPostMakeTransfer([FromBody] TransferViewModel transferInput)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid input" });
            }
            Console.WriteLine($"Data Received: FromAccountId={transferInput.FromAccountId}, ToAccountNumber={transferInput.ToAccountNumber}, Amount={transferInput.Amount}, Comment={transferInput.Comment}");
            try
            {
                var errorCode = _accountService.Transfer(transferInput.FromAccountId, transferInput.ToAccountNumber, transferInput.Amount, transferInput.Comment);
                if (errorCode == ErrorCode.AccountNotFound)
                {
                    return new JsonResult(new { success = false, message = "Transfer failed: Account number not found." });
                }
                else if (errorCode == ErrorCode.BalanceTooLow)
                {
                    return new JsonResult(new { success = false, message = "Transfer failed: Balance too low." });
                }
                else if (errorCode != ErrorCode.OK)
                {
                    return new JsonResult(new { success = false, message = $"Transfer failed: {errorCode}" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Internal server error: {ex.Message}" });
            }

            return new JsonResult(new { success = true, message = "Transfer successful!" });
        }

    }
}

