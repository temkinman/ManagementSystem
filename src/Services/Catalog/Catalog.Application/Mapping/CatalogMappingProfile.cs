using AutoMapper;
using Catalog.Application.Catalogs.Commands.CreateProduct;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mapping;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductDto.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDto.Description))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductDto.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDto.Price))
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryDto != null ?
                new CategoryDto(src.CategoryDto.Name) :
                null));

        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}