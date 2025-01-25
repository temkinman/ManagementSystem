using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.Products.CreateProduct;
using Catalog.Application.Catalogs.Commands.Products.DeleteProduct;
using Catalog.Application.Catalogs.Commands.Products.UpdateProduct;
using Catalog.Application.Catalogs.Queries.GetProductById;

namespace Catalog.Api.Mapping;

public class CatalogRequestProfile : Profile
{
    public CatalogRequestProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<DeleteProductResult, DeleteProductResponse>();
        CreateMap<GetProductByIdResult, GetProductByIdResponse>();
    }
}