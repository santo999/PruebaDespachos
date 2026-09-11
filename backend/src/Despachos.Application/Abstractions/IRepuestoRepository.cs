using Despachos.Application.Repuestos;
using Despachos.Domain.Entities;

namespace Despachos.Application.Abstractions;

public interface IRepuestoRepository
{
    Task<IReadOnlyList<RepuestoDto>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<Repuesto?> ObtenerPorIdAsync(Guid id, CancellationToken ct = default);
}
