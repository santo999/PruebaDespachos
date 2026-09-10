using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Despachos.Application.Abstractions;
using Despachos.Application.Despachos;
using Despachos.Domain.Entities;
using Despachos.Infrastructure.Persistence;

namespace Despachos.Infrastructure.Repositories;

public class DespachoRepository(AppDbContext context, ILogger<DespachoRepository> logger) : IDespachoRepository
{
    public async Task<RegistroDespachoResultado> RegistrarAsync(Despacho despacho, CancellationToken ct = default)
    {

        await using var transaction = await context.Database.BeginTransactionAsync(ct);

        try
        {
            context.Despachos.Add(despacho);
            await context.SaveChangesAsync(ct);
        }

        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync(ct);
            logger.LogWarning(ex,
                "Referencia externa duplicada al registrar despacho: {Referencia}",
                despacho.ReferenciaExterna);
            return RegistroDespachoResultado.ReferenciaExternaDuplicada;
        }

        var filasAfectadas = await context.Repuestos
            .Where(r => r.Id == despacho.RepuestoId && r.CantidadDisponible >= despacho.Cantidad)
            .ExecuteUpdateAsync(setters => setters.SetProperty(
                r => r.CantidadDisponible, r => r.CantidadDisponible - despacho.Cantidad), ct);

        if (filasAfectadas == 0)
        {
            await transaction.RollbackAsync(ct);
            logger.LogInformation(
                "Stock insuficiente al confirmar despacho para repuesto {RepuestoId}", despacho.RepuestoId);
            return RegistroDespachoResultado.StockInsuficiente;
        }

        await transaction.CommitAsync(ct);
        return RegistroDespachoResultado.Registrado;
    }

    public async Task<IReadOnlyList<DespachoDto>> ObtenerHistorialAsync(CancellationToken ct = default)
    {
        return await context.Despachos
            .OrderByDescending(d => d.FechaRegistro)
            .Select(d => new DespachoDto(
                d.Id, d.ReferenciaExterna, d.RepuestoId,
                d.Repuesto!.Sku, d.Repuesto.Nombre, d.Cantidad, d.FechaRegistro))
            .ToListAsync(ct);
    }

}
