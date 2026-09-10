namespace Despachos.Application.Repuestos;

public interface IRepuestoService
{
    Task<IReadOnlyList<RepuestoDto>> ConsultarInventarioAsync(CancellationToken ct = default);
}
