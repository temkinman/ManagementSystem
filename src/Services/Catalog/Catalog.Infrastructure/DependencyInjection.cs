﻿using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Contexts;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddDbContext<ICatalogDbContext,CatalogDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(nameof(CatalogDbContext)))
                .LogTo(Console.WriteLine));
        
        return services;
    }
}