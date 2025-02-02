using AutoMapper;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Catalogs.Commands.Products.CreateProduct;
using Catalog.Application.Catalogs.Commands.Products.UpdateProduct;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mapping;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Product.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category != null ?
                new CategoryDto(src.Category.Name) :
                null));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryCommand, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));
        CreateMap<UpdateCategoryCommand, Category>();
        
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}