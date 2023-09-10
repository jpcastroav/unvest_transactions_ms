using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;

namespace unvest_transactions_ms.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly TransactionsContext _context;

        public OperacionController(TransactionsContext context)
        {
            _context = context;
        }

        // GET: Operacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
          if (_context.Operacion == null)
          {
              return NotFound();
          }
            return await _context.Operacion.ToListAsync();
        }

        // GET: Operacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
          if (_context.Operacion == null)
          {
              return NotFound();
          }
            var operacion = await _context.Operacion.FindAsync(id);

            if (operacion == null)
            {
                return NotFound();
            }

            return operacion;
        }

        // PUT: Operacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, Operacion operacion)
        {
            if (id != operacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(operacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperacionExists(id))
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

        // POST: Operacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Operacion>> PostOperacion(Operacion operacion)
        {
          if (_context.Operacion == null)
          {
              return Problem("Entity set 'TransactionsContext.Operacion'  is null.");
          }
            _context.Operacion.Add(operacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperacion", new { id = operacion.Id }, operacion);
        }

        // DELETE: Operacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            if (_context.Operacion == null)
            {
                return NotFound();
            }
            var operacion = await _context.Operacion.FindAsync(id);
            if (operacion == null)
            {
                return NotFound();
            }

            _context.Operacion.Remove(operacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperacionExists(int id)
        {
            return (_context.Operacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
