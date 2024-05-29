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
        public AccountViewModel Account { get; set; }
        public List<Transaction>;
        public void OnGet()
        {
        }
    }
}
