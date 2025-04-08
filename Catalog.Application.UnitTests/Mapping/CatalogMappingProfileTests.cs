using AutoMapper;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Dtos;
using Catalog.Application.Mapping;
using Catalog.Domain.Entities;
using FluentAssertions;

namespace Catalog.Tests.Mapping;

public class CatalogMappingProfileTests
{
    private readonly IMapper _mapper;

    public CatalogMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => 
            cfg.AddProfile<CatalogMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateCategoryCommand_ShouldMapTo_Category()
    {
        // Arrange
        var command = new CreateCategoryCommand("Electronics");

        // Act
        var category = _mapper.Map<Category>(command);

        // Assert
        category.Name.Should().Be("Electronics");
        category.Id.Should().BeEmpty(); // т.к. Ignore()
    }

    [Fact]
    public void ProductDto_ShouldMapTo_Product()
    {
        // Arrange
        var dto = new ProductDto(
            Name: "Laptop",
            Description: "Gaming Laptop",
            Price: 999.99m,
            Quantity: 10,
            Category: new CategoryDto("Electronics"));

        // Act
        var product = _mapper.Map<Product>(dto);

        // Assert
        product.Name.Should().Be("Laptop");
        product.Description.Should().Be("Gaming Laptop");
        product.Price.Should().Be(999.99m);
        product.Quantity.Should().Be(10);
    }
}