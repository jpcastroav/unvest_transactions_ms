using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;

namespace unvest_transactions_ms.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly TransactionsContext _context;

        public BalanceController(TransactionsContext context)
        {
            _context = context;
        }

        // GET: Balance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Balance>>> GetBalance()
        {
          if (_context.Balance == null)
          {
              return NotFound();
          }
            return await _context.Balance.ToListAsync();
        }

        // GET: Balance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetBalance(int id)
        {
          if (_context.Balance == null)
          {
              return NotFound();
          }
            var balance = await _context.Balance.FindAsync(id);

            if (balance == null)
            {
                return NotFound();
            }

            return balance;
        }

        // PUT: Balance/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBalance(int id, Balance balance)
        {
            if (id != balance.Id)
            {
                return BadRequest();
            }

            _context.Entry(balance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BalanceExists(id))
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

        // POST: Balance
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Balance>> PostBalance(Balance balance)
        {
          if (_context.Balance == null)
          {
              return Problem("Entity set 'TransactionsContext.Balance'  is null.");
          }
            _context.Balance.Add(balance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBalance", new { id = balance.Id }, balance);
        }

        // DELETE: Balance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBalance(int id)
        {
            if (_context.Balance == null)
            {
                return NotFound();
            }
            var balance = await _context.Balance.FindAsync(id);
            if (balance == null)
            {
                return NotFound();
            }

            _context.Balance.Remove(balance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BalanceExists(int id)
        {
            return (_context.Balance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
