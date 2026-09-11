using Despachos.Application.Abstractions;

namespace Despachos.Application.Repuestos;

public sealed class RepuestoService(IRepuestoRepository repuestoRepository) : IRepuestoService
{

    public Task<IReadOnlyList<RepuestoDto>> ConsultarInventarioAsync(CancellationToken ct = default)
    {
        return repuestoRepository.ObtenerTodosAsync(ct);
    }
}

