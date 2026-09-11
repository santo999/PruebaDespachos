using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Despachos.Api.Tests;

public class ApiFactory : WebApplicationFactory<Program>
{

    private static string ObtenerConnectionStringDePrueba() =>
        Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING")
            ?? "Server=;Database=;Trusted_Connection=True;TrustServerCertificate=True;";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = ObtenerConnectionStringDePrueba(),
            });
        });
    }
}
