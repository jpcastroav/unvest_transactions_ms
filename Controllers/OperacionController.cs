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

        // POST: Operacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Operacion>> PostOperacion(Operacion operacion)
        {
            if (_context.Operacion == null)
            {
                return Problem("Entity set 'TransactionsContext.Operacion'  is null.");
            }

            //Se calcula el balance actual de acuerdo a las operaciones de deposito y retiro realizadas
            decimal balanceActual = _context.Operacion.Where(t => t.Tipo == 1 && t.IdUsuario == operacion.IdUsuario).Sum(s => s.Cantidad);
            balanceActual -= _context.Operacion.Where(t => t.Tipo == 2 && t.IdUsuario == operacion.IdUsuario).Sum(s => s.Cantidad);

            if(operacion.Tipo == 2 && operacion.Cantidad > balanceActual)//Retiro. Se debe restringir a la cantidad disponible
            {
                ModelState.AddModelError("Cantidad", "La cantidad solicitada supera la cantidad de retiro");
                return BadRequest(ModelState);
            }

            //Si se trata de un deposito o un retiro que no supera el balance actual, se actualiza el balance
            var balance = _context.Balance.FirstOrDefault<Balance>(b => b.IdUsuario == operacion.IdUsuario);
            if(balance == null)
            {
                balance = new Balance{IdUsuario = operacion.IdUsuario};
                _context.Entry(balance).State = EntityState.Added;
            }
            else
            {
                _context.Entry(balance).State = EntityState.Modified;
            }
            balance.Valor = operacion.Tipo == 1? balanceActual + operacion.Cantidad : balanceActual - operacion.Cantidad;
            //Se agrega la operacion a la base de datos
            _context.Operacion.Add(operacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperacion", new { id = operacion.Id }, operacion);
        }

        private bool OperacionExists(int id)
        {
            return (_context.Operacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
