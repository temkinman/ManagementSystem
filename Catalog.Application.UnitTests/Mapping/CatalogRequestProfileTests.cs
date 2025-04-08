using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Api.Mapping;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Catalogs.Commands.Products.CreateProduct;
using Catalog.Application.Dtos;
using FluentAssertions;

namespace Catalog.Tests.Mapping;

public class CatalogRequestProfileTests
{
    private readonly IMapper _mapper;

    public CatalogRequestProfileTests()
    {
        var config = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile<CatalogRequestProfile>();
        });
        _mapper = config.CreateMapper();
    }
    
    [Fact]
    public void CreateProductRequest_ShouldMapTo_CreateProductCommand()
    {
        // Arrange
        var category = new CategoryDto("Bear");
        
        var request = new CreateProductRequest(
            new ProductDto(
                Name: "Laptop",
                Description: "Gaming Laptop",
                Price: 999.99m,
                Quantity: 10,
                Category: category
            ));

        // Act
        var command = _mapper.Map<CreateProductCommand>(request);

        // Assert
        command.Should().NotBeNull();
        command.Product.Name.Should().Be("Laptop");
        command.Product.Price.Should().Be(999.99m);
    }

    [Fact]
    public void UpdateCategoryRequest_ShouldMapTo_UpdateCategoryCommand()
    {
        // Arrange
        var request = new UpdateCategoryRequest(
            Id: Guid.NewGuid(),
            Name: "New Category Name");

        // Act
        var command = _mapper.Map<UpdateCategoryCommand>(request);

        // Assert
        command.Should().NotBeNull();
        command.Id.Should().Be(request.Id);
        command.Name.Should().Be("New Category Name");
    }
}