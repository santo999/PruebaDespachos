using Despachos.Application.Despachos;
using Despachos.Domain.Entities;

namespace Despachos.Application.Abstractions;

public enum RegistroDespachoResultado
{
    Registrado,
    ReferenciaExternaDuplicada,
    StockInsuficiente
}

public interface IDespachoRepository
{
    Task<RegistroDespachoResultado> RegistrarAsync(Despacho despacho, CancellationToken ct = default);
    Task<IReadOnlyList<DespachoDto>> ObtenerHistorialAsync(CancellationToken ct = default);
}
