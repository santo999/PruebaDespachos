using System.ComponentModel.DataAnnotations;

namespace Despachos.Application.Despachos;

public sealed record RegistrarDespachoRequest(

    [Required(AllowEmptyStrings = false), StringLength(100, MinimumLength = 1)]
    string ReferenciaExterna,

    Guid RepuestoId,

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    int Cantidad);
