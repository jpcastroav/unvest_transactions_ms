using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;

namespace unvest_transactions_ms.Controllers
{
    public class TransaccionAsyncController
    {
        private readonly TransactionsContext _context;

        public TransaccionAsyncController(TransactionsContext context)
        {
            _context = context;
        }

        public async Task<List<Transaccion>> GetTransaccion()
        {
          if (_context.Transaccion == null)
          {
              return new List<Transaccion>();
          }
            return await _context.Transaccion.ToListAsync();
        }
    }
}