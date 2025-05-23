using FinancialAccount.Api.Data;
using FinancialAccount.Api.DTOs;
using FinancialAccount.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccount.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly FinancialDbContext _db;

        public AccountsController(FinancialDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            return await _db.Accounts
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    AccountNumber = a.AccountNumber,
                    AccountHolder = a.AccountHolder,
                    Balance = a.Balance
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var account = await _db.Accounts.FindAsync(id);

            if (account == null)
                return NotFound();

            return new AccountDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountHolder = account.AccountHolder,
                Balance = account.Balance
            };
        }

        [HttpPost]
        public async Task<ActionResult<AccountDTO>> CreateAccount(AccountDTO dto)
        {
            var account = new Account
            {
                AccountNumber = dto.AccountNumber,
                AccountHolder = dto.AccountHolder,
                Balance = dto.Balance
            };

            _db.Accounts.Add(account);
            await _db.SaveChangesAsync();

            dto.Id = account.Id;

            return CreatedAtAction(nameof(GetAccount), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var account = await _db.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            account.AccountNumber = dto.AccountNumber;
            account.AccountHolder = dto.AccountHolder;
            account.Balance = dto.Balance;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _db.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            _db.Accounts.Remove(account);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }

}
