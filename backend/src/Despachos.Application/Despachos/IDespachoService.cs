namespace Despachos.Application.Despachos;

public interface IDespachoService
{
    Task<DespachoDto> RegistrarDespachoAsync(RegistrarDespachoRequest request, CancellationToken ct = default);

    Task<IReadOnlyList<DespachoDto>> ConsultarHistorialAsync(CancellationToken ct = default);
}
