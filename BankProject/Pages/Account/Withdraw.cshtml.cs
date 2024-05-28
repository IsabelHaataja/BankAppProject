using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.ComponentModel.DataAnnotations;

namespace BankProject.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
        public WithdrawModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
		[Range(100, 10000)]
		public decimal Amount { get; set; }

		public decimal Balance { get; set; }
		public void OnGet(int accountId)
        {
            Balance = _accountService.GetAccount(accountId).Balance;
        }
        public IActionResult OnPost(int accountId)
        {
            var status = _accountService.Withdraw(accountId, Amount);

            if (ModelState.IsValid)
            { 
                if (status == Services.Infrastructure.ErrorCode.OK)
                {
                    return RedirectToPage("Index");
                }
            }
            if (status == Services.Infrastructure.ErrorCode.BalanceTooLow)
            {
                ModelState.AddModelError("Amount", "The balance is too low.");
            }
            if (status == Services.Infrastructure.ErrorCode.IncorrectAmount)
            {
                ModelState.AddModelError("Amount", "Please enter a valid amount (100-10000).");
            }
            return Page();
        }
    }
}
