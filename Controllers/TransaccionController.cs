using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;

namespace unvest_transactions_ms.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly TransactionsContext _context;

        public TransaccionController(TransactionsContext context)
        {
            _context = context;
        }

        // GET: Transaccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccion()
        {
          if (_context.Transaccion == null)
          {
              return NotFound();
          }
            return await _context.Transaccion.ToListAsync();
        }

        // GET: Transaccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
        {
          if (_context.Transaccion == null)
          {
              return NotFound();
          }
            var transaccion = await _context.Transaccion.FindAsync(id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return transaccion;
        }

        // PUT: Transaccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccion(int id, Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(id))
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

        // POST: Transaccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaccion>> PostTransaccion(Transaccion transaccion)
        {
          if (_context.Transaccion == null)
          {
              return Problem("Entity set 'TransactionsContext.Transaccion'  is null.");
          }
            _context.Transaccion.Add(transaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaccion", new { id = transaccion.Id }, transaccion);
        }

        // DELETE: Transaccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            if (_context.Transaccion == null)
            {
                return NotFound();
            }
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transaccion.Remove(transaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccionExists(int id)
        {
            return (_context.Transaccion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
