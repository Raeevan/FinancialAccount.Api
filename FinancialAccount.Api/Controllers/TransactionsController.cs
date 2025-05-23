using FinancialAccount.Api.Data;
using FinancialAccount.Api.DTOs;
using FinancialAccount.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccount.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly FinancialDbContext _db;

        public TransactionsController(FinancialDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            return await _db.Transactions
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    AccountId = t.AccountId,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            return new TransactionDTO
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate
            };
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction(TransactionCreateDTO dto)
        {
            var account = await _db.Accounts.FindAsync(dto.AccountId);
            if (account == null)
                return BadRequest("Account not found.");

            //  Validate transaction type
            if (dto.TransactionType != "Deposit" && dto.TransactionType != "Withdrawal")
                return BadRequest("Invalid transaction type. Must be 'Deposit' or 'Withdrawal'.");

            // Handle business logic
            if (dto.TransactionType == "Deposit")
            {
                account.Balance += dto.Amount;
            }
            else if (dto.TransactionType == "Withdrawal")
            {
                if (account.Balance < dto.Amount)
                    return BadRequest("Insufficient funds.");

                account.Balance -= dto.Amount;
            }

            //  Create transaction record
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionDate = DateTime.UtcNow
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            // Return created transaction
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, new TransactionDTO
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            var account = await _db.Accounts.FindAsync(transaction.AccountId);
            if (account == null)
                return BadRequest("Associated account not found.");

            
            if (transaction.TransactionType == "Deposit")
                account.Balance -= transaction.Amount;
            else if (transaction.TransactionType == "Withdrawal")
                account.Balance += transaction.Amount;

            if (dto.TransactionType == "Deposit")
            {
                account.Balance += dto.Amount;
            }
            else if (dto.TransactionType == "Withdrawal")
            {
                if (account.Balance < dto.Amount)
                    return BadRequest("Insufficient funds for updated withdrawal.");
                account.Balance -= dto.Amount;
            }
            else
            {
                return BadRequest("Invalid transaction type.");
            }

            
            transaction.TransactionType = dto.TransactionType;
            transaction.Amount = dto.Amount;
            transaction.TransactionDate = dto.TransactionDate;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }

}
