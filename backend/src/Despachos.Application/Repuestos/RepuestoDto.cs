namespace Despachos.Application.Repuestos;

public sealed record RepuestoDto(Guid Id, string Sku, string Nombre, int CantidadDisponible);
