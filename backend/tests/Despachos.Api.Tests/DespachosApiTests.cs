using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Despachos.Application.Despachos;
using Despachos.Application.Repuestos;

namespace Despachos.Api.Tests;


public class DespachosApiTests(ApiFactory factory) : IClassFixture<ApiFactory>
{

    private static readonly JsonSerializerOptions JsonOpciones = new() { PropertyNameCaseInsensitive = true };

    private readonly HttpClient _cliente = factory.CreateClient();

    private async Task<RepuestoDto> ObtenerRepuestoPorSkuAsync(string sku)
    {
        var repuestos = await _cliente.GetFromJsonAsync<List<RepuestoDto>>("/api/repuestos", JsonOpciones);
        return repuestos!.Single(r => r.Sku == sku);
    }

    [Fact]
    public async Task Despacho_Valido_DescuentaStockYQuedaEnHistorial()
    {

        var repuestoAntes = await ObtenerRepuestoPorSkuAsync("RPT-001");
        var referencia = $"TEST-EXITO-{Guid.NewGuid()}";

        var respuesta = await _cliente.PostAsJsonAsync(
            "/api/despachos",
            new RegistrarDespachoRequest(referencia, repuestoAntes.Id, 1));

        Assert.Equal(HttpStatusCode.Created, respuesta.StatusCode);

        var despachoCreado = await respuesta.Content.ReadFromJsonAsync<DespachoDto>(JsonOpciones);
        Assert.Equal(referencia, despachoCreado!.ReferenciaExterna);
        Assert.Equal(1, despachoCreado.Cantidad);


        var repuestoDespues = await ObtenerRepuestoPorSkuAsync("RPT-001");
        Assert.Equal(repuestoAntes.CantidadDisponible - 1, repuestoDespues.CantidadDisponible);

        var historial = await _cliente.GetFromJsonAsync<List<DespachoDto>>("/api/despachos", JsonOpciones);
        Assert.Contains(historial!, d => d.ReferenciaExterna == referencia);
    }

    [Fact]
    public async Task Despacho_StockInsuficiente_NoAlteraInventarioNiRegistraDespacho()
    {
        var repuestoAntes = await ObtenerRepuestoPorSkuAsync("RPT-002");
        var referencia = $"TEST-STOCK-{Guid.NewGuid()}";


        var respuesta = await _cliente.PostAsJsonAsync(
            "/api/despachos",
            new RegistrarDespachoRequest(referencia, repuestoAntes.Id, repuestoAntes.CantidadDisponible + 1_000_000));

        Assert.Equal(HttpStatusCode.Conflict, respuesta.StatusCode);

        var problema = await respuesta.Content.ReadFromJsonAsync<ProblemDetails>(JsonOpciones);
        Assert.Equal("Stock insuficiente", problema!.Title);

        var repuestoDespues = await ObtenerRepuestoPorSkuAsync("RPT-002");
        Assert.Equal(repuestoAntes.CantidadDisponible, repuestoDespues.CantidadDisponible);

        var historial = await _cliente.GetFromJsonAsync<List<DespachoDto>>("/api/despachos", JsonOpciones);
        Assert.DoesNotContain(historial!, d => d.ReferenciaExterna == referencia);
    }

    [Fact]
    public async Task Despacho_ReferenciaExternaDuplicada_NoDuplicaElDescuento()
    {
        var repuestoAntes = await ObtenerRepuestoPorSkuAsync("RPT-003");
        var referencia = $"TEST-DUP-{Guid.NewGuid()}";
        var request = new RegistrarDespachoRequest(referencia, repuestoAntes.Id, 1);

        var primeraRespuesta = await _cliente.PostAsJsonAsync("/api/despachos", request);
        Assert.Equal(HttpStatusCode.Created, primeraRespuesta.StatusCode);

        var segundaRespuesta = await _cliente.PostAsJsonAsync("/api/despachos", request);
        Assert.Equal(HttpStatusCode.Conflict, segundaRespuesta.StatusCode);

        var problema = await segundaRespuesta.Content.ReadFromJsonAsync<ProblemDetails>(JsonOpciones);
        Assert.Equal("Solicitud ya procesada", problema!.Title);

        var repuestoDespues = await ObtenerRepuestoPorSkuAsync("RPT-003");
        Assert.Equal(repuestoAntes.CantidadDisponible - 1, repuestoDespues.CantidadDisponible);
    }
}
