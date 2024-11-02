using FinancialTransactionsAPI.DataContext;
using FinancialTransactionsAPI.Enums;
using FinancialTransactionsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/transactions - Create a Transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
        }

        // GET: api/transactions - Get All Transactions with Optional Filtering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions(
            [FromQuery] TransactionStatus? status = null,
            [FromQuery] TransactionType? type = null)
        {
            var transactions = _context.Transactions.AsQueryable();

            if (status.HasValue)
            {
                transactions = transactions.Where(t => t.Status == status.Value);
            }

            if (type.HasValue)
            {
                transactions = transactions.Where(t => t.TransactionType == type.Value);
            }

            return await transactions.ToListAsync();
        }

        // GET: api/transactions/{id} - Get a Transaction by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/transactions/{id} - Update a Transaction
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
        {
            if (id != updatedTransaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(updatedTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/transactions/{id} - Delete a Transaction
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/transactions/summary - Get Transaction Summary
        [HttpGet("summary")]
        public async Task<ActionResult> GetTransactionSummary([FromQuery] string? customerName = null)
        {
            var transactions = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                transactions = transactions.Where(t => t.CustomerFullName == customerName);
            }

            var totalCredits = await transactions.Where(t => t.TransactionType == TransactionType.Credit).SumAsync(t => t.Amount);
            var totalDebits = await transactions.Where(t => t.TransactionType == TransactionType.Debit).SumAsync(t => t.Amount);
            var count = await transactions.CountAsync();

            var summary = new
            {
                TotalTransactions = count,
                TotalCredits = totalCredits,
                TotalDebits = totalDebits,
                NetBalance = totalCredits - totalDebits
            };

            return Ok(summary);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
