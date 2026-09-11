using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Despachos.Application.Abstractions;
using Despachos.Infrastructure.Persistence;
using Despachos.Infrastructure.Repositories;

namespace Despachos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));


        services.AddScoped<IRepuestoRepository, RepuestoRepository>();
        services.AddScoped<IDespachoRepository, DespachoRepository>();
        return services;
    }
}
