using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Catalogs.Commands.Categories.DeleteProduct;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Catalogs.Commands.Products.CreateProduct;
using Catalog.Application.Catalogs.Commands.Products.DeleteProduct;
using Catalog.Application.Catalogs.Commands.Products.UpdateProduct;
using Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryById;
using Catalog.Application.Catalogs.Queries.GetProductById;
using Catalog.Application.Dtos;

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
        CreateMap<UpdateProductResult, UpdateProductResponse>();
        CreateMap<UpdateProductResult, UpdateCategoryResponse>();
        CreateMap<CreateCategoryResult, CreateCategoryResponse>();
        CreateMap<GetCategoryByIdResult, CategoryDto>();
        CreateMap<DeleteCategoryResult, DeleteCategoryResponse>();
        CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
        CreateMap<UpdateCategoryResult, UpdateCategoryResponse>();
    }
}