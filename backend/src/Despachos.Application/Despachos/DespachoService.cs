using Microsoft.Extensions.Logging;
using Despachos.Application.Abstractions;
using Despachos.Application.Common.Exceptions;
using Despachos.Domain.Entities;

namespace Despachos.Application.Despachos;


public sealed class DespachoService(
    IRepuestoRepository repuestoRepository,
    IDespachoRepository despachoRepository,
    ILogger<DespachoService> logger) : IDespachoService
{
    public async Task<DespachoDto> RegistrarDespachoAsync(RegistrarDespachoRequest request, CancellationToken ct = default)
    {

        if (request.Cantidad <= 0)
        {
            logger.LogWarning(
                "Despacho rechazado: cantidad inválida {Cantidad} (referencia {Referencia})",
                request.Cantidad, request.ReferenciaExterna);

            throw new CantidadInvalidaException(request.Cantidad);
        }

        var repuesto = await repuestoRepository.ObtenerPorIdAsync(request.RepuestoId, ct);
        if (repuesto is null)
        {
            logger.LogWarning(
                "Despacho rechazado: repuesto {RepuestoId} no existe (referencia {Referencia})",
                request.RepuestoId, request.ReferenciaExterna);
            throw new RepuestoNoEncontradoException(request.RepuestoId);
        }

        var despacho = Despacho.Crear(request.ReferenciaExterna, request.RepuestoId, request.Cantidad);

        var resultado = await despachoRepository.RegistrarAsync(despacho, ct);

        switch (resultado)
        {
            case RegistroDespachoResultado.ReferenciaExternaDuplicada:

                logger.LogInformation(
                    "Despacho rechazado: referencia externa {Referencia} ya fue procesada",
                    request.ReferenciaExterna);
                throw new ReferenciaExternaDuplicadaException(request.ReferenciaExterna);

            case RegistroDespachoResultado.StockInsuficiente:
                logger.LogInformation(
                    "Despacho rechazado: stock insuficiente para repuesto {RepuestoId} (solicitado {Cantidad}, disponible {Disponible})",
                    request.RepuestoId, request.Cantidad, repuesto.CantidadDisponible);
                throw new StockInsuficienteException(repuesto.Nombre, request.Cantidad, repuesto.CantidadDisponible);
        }


        logger.LogInformation(
            "Despacho {DespachoId} registrado (referencia {Referencia}, repuesto {RepuestoId}, cantidad {Cantidad})",
            despacho.Id, despacho.ReferenciaExterna, repuesto.Id, despacho.Cantidad);

        return new DespachoDto(
            despacho.Id, despacho.ReferenciaExterna, repuesto.Id, repuesto.Sku, repuesto.Nombre,
            despacho.Cantidad, despacho.FechaRegistro);
    }

    public Task<IReadOnlyList<DespachoDto>> ConsultarHistorialAsync(CancellationToken ct = default)
    {
        return despachoRepository.ObtenerHistorialAsync(ct);
    }

}
