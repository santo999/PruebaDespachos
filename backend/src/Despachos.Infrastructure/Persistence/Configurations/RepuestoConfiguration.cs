using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Despachos.Domain.Entities;

namespace Despachos.Infrastructure.Persistence.Configurations;

public class RepuestoConfiguration : IEntityTypeConfiguration<Repuesto>
{
    public void Configure(EntityTypeBuilder<Repuesto> builder)
    {

        builder.ToTable("Repuestos");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.Property(r => r.Sku)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(r => r.Sku).IsUnique();

        builder.Property(r => r.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.CantidadDisponible)
            .IsRequired();

        builder.HasData(
            new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Sku = "RPT-001", Nombre = "Rodamiento 62-2RS", CantidadDisponible = 50 },
            new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Sku = "RPT-002", Nombre = "Filtro de aceite", CantidadDisponible = 30 },
            new { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Sku = "RPT-003", Nombre = "Correa A-60", CantidadDisponible = 20 });
    }
}
