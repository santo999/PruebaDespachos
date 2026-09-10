using Microsoft.EntityFrameworkCore;
using Despachos.Domain.Entities;

namespace Despachos.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Repuesto> Repuestos => Set<Repuesto>();
    public DbSet<Despacho> Despachos => Set<Despacho>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
