using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;

using System.Text.Json.Serialization;

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

        // GET: Transaccion/getOwnedStocks/5
        [HttpGet("getOwnedStocks/{userId}")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetOwnedStocks(int userId)
        {
            if (_context.Transaccion == null)
            {
                return NotFound();
            }

            var transacciones = _context.Transaccion.Where<Transaccion>(t => t.IdUsuario == userId).Select(s => new Stock{IdEmpresa = s.IdEmpresa, Cantidad = s.Tipo == 1? s.Cantidad : (-1)*s.Cantidad }).GroupBy(g => g.IdEmpresa);

            var stocks = new List<Stock>();

            foreach (var group in transacciones)
            {
                var stock = new Stock{IdEmpresa = group.Key, Cantidad = group.Sum(s => s.Cantidad)};
                stocks.Add(stock);
            }

            Console.WriteLine("Empresa " + stocks[0].IdEmpresa + ", Cantidad " + stocks[0].Cantidad);

            return stocks;
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

            var balance = _context.Balance.FirstOrDefault<Balance>(b => b.IdUsuario == transaccion.IdUsuario);

            if(balance == null)
            {
                ModelState.AddModelError("Balance", "No se pudo realizar la acción porque no se pudo obtener la información del balance");
                return BadRequest(ModelState);
            }

            if(transaccion.Tipo == 1)//Compra
            {
                decimal cantidadCompra = transaccion.Cantidad * transaccion.ValorAccion;

                if(cantidadCompra > balance.Valor)
                {
                    ModelState.AddModelError("Valor compra", "El valor total de la compra excede el balance actual");
                    return BadRequest(ModelState);
                }

                balance.Valor -= cantidadCompra;
                 _context.Entry(balance).State = EntityState.Modified;
            }
            else //Venta 
            {
                decimal disponible = _context.Transaccion.Where(t => t.Tipo == 1 && t.IdEmpresa == transaccion.IdEmpresa && t.IdUsuario == transaccion.IdUsuario).Sum(s => s.Cantidad);
                disponible -= _context.Transaccion.Where(t => t.Tipo == 2 && t.IdEmpresa == transaccion.IdEmpresa && t.IdUsuario == transaccion.IdUsuario).Sum(s => s.Cantidad);

                if(disponible <= 0 || transaccion.Cantidad > disponible)
                {
                    ModelState.AddModelError("Cantidad venta", "La cantidad de la venta supera el disponible de acciones");
                    return BadRequest(ModelState);
                }

                balance.Valor += transaccion.Cantidad * transaccion.ValorAccion;
            }

            _context.Transaccion.Add(transaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaccion", new { id = transaccion.Id }, transaccion);
        }

        public class Stock
        {
            [JsonPropertyName("id_empresa")]
            public int IdEmpresa {get; set;}

            [JsonPropertyName("cantidad")]
            public decimal Cantidad {get; set;}
        }

        private bool TransaccionExists(int id)
        {
            return (_context.Transaccion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
