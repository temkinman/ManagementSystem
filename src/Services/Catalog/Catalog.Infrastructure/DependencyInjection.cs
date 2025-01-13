using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<ICatalogDbContext, CatalogDbContext>();
        services.AddDbContext<CatalogDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(nameof(CatalogDbContext))));
        
        return services;
    }
}