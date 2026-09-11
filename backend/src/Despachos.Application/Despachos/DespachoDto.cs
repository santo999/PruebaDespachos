namespace Despachos.Application.Despachos;

public sealed record DespachoDto(
    Guid Id,
    string ReferenciaExterna,
    Guid RepuestoId,
    string RepuestoSku,
    string RepuestoNombre,
    int Cantidad,
    DateTime FechaRegistro);
