using Microsoft.EntityFrameworkCore;
using Despachos.Application.Abstractions;
using Despachos.Application.Repuestos;
using Despachos.Domain.Entities;
using Despachos.Infrastructure.Persistence;

namespace Despachos.Infrastructure.Repositories;

public class RepuestoRepository(AppDbContext context) : IRepuestoRepository
{
    public async Task<IReadOnlyList<RepuestoDto>> ObtenerTodosAsync(CancellationToken ct = default)
    {
        return await context.Repuestos
            .OrderBy(r => r.Sku)
            .Select(r => new RepuestoDto(r.Id, r.Sku, r.Nombre, r.CantidadDisponible))
            .ToListAsync(ct);
    }

    public Task<Repuesto?> ObtenerPorIdAsync(Guid id, CancellationToken ct = default)
    {
        return context.Repuestos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, ct);
    }
}
