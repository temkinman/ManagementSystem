using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.CreateProduct;

namespace Catalog.Api.Mapping;

public class CatalogRequestProfile : Profile
{
    public CatalogRequestProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}