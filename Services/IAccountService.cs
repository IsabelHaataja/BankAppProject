using DataAccessLayer.Models;
using Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public interface IAccountService
	{
		Account GetAccount(int accountId);
		ErrorCode Withdraw(int accountId, decimal amount);
		ErrorCode Deposit(int accountId, decimal amount, string comment);
	}
}
