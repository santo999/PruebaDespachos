using Microsoft.AspNetCore.Mvc;
using Despachos.Application.Despachos;

namespace Despachos.Api.Controllers;

[ApiController]
[Route("api/despachos")]
public sealed class DespachosController(IDespachoService despachoService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(DespachoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DespachoDto>> Registrar([FromBody] RegistrarDespachoRequest request, CancellationToken ct)
    {

        var despacho = await despachoService.RegistrarDespachoAsync(request, ct);


        return StatusCode(StatusCodes.Status201Created, despacho);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DespachoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DespachoDto>>> ObtenerHistorial(CancellationToken ct)
    {
        return Ok(await despachoService.ConsultarHistorialAsync(ct));
    }

}
