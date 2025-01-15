using System.Reflection;
using Catalog.Application.Catalogs.Commands;
using Catalog.Application.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CatalogMappingProfile).Assembly);

        return services;
    }
}