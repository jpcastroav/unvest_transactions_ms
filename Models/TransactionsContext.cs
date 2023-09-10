using Microsoft.EntityFrameworkCore;

namespace unvest_transactions_ms.Models;

public class TransactionsContext : DbContext
{
    public TransactionsContext (DbContextOptions<TransactionsContext> options)
        : base(options)
    {
    }

    public DbSet<Balance> Balance { get; set; } = default!;

    public DbSet<Transaccion> Transaccion { get; set; } = default!;

    public DbSet<Operacion> Operacion { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>().Property(e => e.Valor).HasColumnType("decimal");
        modelBuilder.Entity<Balance>().Property(e => e.Valor).HasPrecision(19,4);

        modelBuilder.Entity<Operacion>().Property(e => e.Cantidad).HasColumnType("decimal");
        modelBuilder.Entity<Operacion>().Property(e => e.Cantidad).HasPrecision(19,4);

        modelBuilder.Entity<Transaccion>().Property(e => e.Cantidad).HasColumnType("decimal");
        modelBuilder.Entity<Transaccion>().Property(e => e.Cantidad).HasPrecision(19,4);

        modelBuilder.Entity<Transaccion>().Property(e => e.ValorAccion).HasColumnType("decimal");
        modelBuilder.Entity<Transaccion>().Property(e => e.ValorAccion).HasPrecision(19,4);
        
        base.OnModelCreating(modelBuilder);
    }
}
