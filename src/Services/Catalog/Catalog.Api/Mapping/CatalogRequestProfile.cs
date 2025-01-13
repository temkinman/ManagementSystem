using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands;

namespace Catalog.Api.Mapping;

public class CatalogRequestProfile : Profile
{
    public CatalogRequestProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}