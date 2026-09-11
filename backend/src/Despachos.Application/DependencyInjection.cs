using Microsoft.Extensions.DependencyInjection;
using Despachos.Application.Despachos;
using Despachos.Application.Repuestos;

namespace Despachos.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRepuestoService, RepuestoService>();
        services.AddScoped<IDespachoService, DespachoService>();

        return services;
    }
}
