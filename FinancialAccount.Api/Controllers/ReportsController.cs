using FinancialAccount.Api.Data;
using FinancialAccount.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly FinancialDbContext _db;

        public ReportsController(FinancialDbContext db)
        {
            _db = db;
        }

        //  Retrieve all transactions for a specific account
        [HttpGet("transactions/by-account/{accountId}")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _db.Transactions
                .Where(t => t.AccountId == accountId)
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    AccountId = t.AccountId,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate
                })
                .ToListAsync();

            return Ok(transactions);
        }

        // Retrieve the total balance of all accounts
        [HttpGet("accounts/total-balance")]
        public async Task<ActionResult<decimal>> GetTotalBalance()
        {
            var totalBalance = await _db.Accounts.SumAsync(a => a.Balance);
            return Ok(totalBalance);
        }

        // Retrieve all accounts with a balance below a certain threshold (e.g., $100)
        [HttpGet("accounts/below-balance/{threshold}")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsBelowThreshold(decimal threshold)
        {
            var accounts = await _db.Accounts
                .Where(a => a.Balance < threshold)
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    AccountNumber = a.AccountNumber,
                    AccountHolder = a.AccountHolder,
                    Balance = a.Balance
                })
                .ToListAsync();

            return Ok(accounts);
        }

        // Retrieve the top 5 accounts with the highest balances
        [HttpGet("accounts/top")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetTopAccounts()
        {
            var topAccounts = await _db.Accounts
                .OrderByDescending(a => a.Balance)
                .Take(5)
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    AccountNumber = a.AccountNumber,
                    AccountHolder = a.AccountHolder,
                    Balance = a.Balance
                })
                .ToListAsync();

            return Ok(topAccounts);
        }
    }
}
