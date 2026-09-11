using Microsoft.AspNetCore.Mvc;
using Despachos.Application.Repuestos;

namespace Despachos.Api.Controllers;

[ApiController]
[Route("api/repuestos")]
public sealed class RepuestosController(IRepuestoService repuestoService) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RepuestoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RepuestoDto>>> ObtenerInventario(CancellationToken ct)
    {
        return Ok(await repuestoService.ConsultarInventarioAsync(ct));
    }
}
