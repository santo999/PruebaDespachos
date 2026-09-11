using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Despachos.Domain.Entities;

namespace Despachos.Infrastructure.Persistence.Configurations;

public class DespachoConfiguration : IEntityTypeConfiguration<Despacho>
{
    public void Configure(EntityTypeBuilder<Despacho> builder)
    {
        builder.ToTable("Despachos");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.ReferenciaExterna)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(d => d.ReferenciaExterna).IsUnique();

        builder.Property(d => d.Cantidad)
            .IsRequired();

        builder.Property(d => d.FechaRegistro)
            .IsRequired();

        builder.HasOne(d => d.Repuesto)
            .WithMany()
            .HasForeignKey(d => d.RepuestoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
