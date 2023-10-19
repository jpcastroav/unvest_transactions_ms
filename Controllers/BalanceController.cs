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

        // GET: Balance/byUserId/5
        [HttpGet("byUserId/{userId}")]
        public async Task<ActionResult<Balance>> GetBalanceByUserId(int userId)
        {
            if (_context.Balance == null)
            {
                return NotFound();
            }

            var balance = await _context.Balance.FirstOrDefaultAsync<Balance>(b => b.IdUsuario == userId);

            if (balance == null)
            {
                return NotFound();
            }

            return balance;
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

        private bool BalanceExists(int id)
        {
            return (_context.Balance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
