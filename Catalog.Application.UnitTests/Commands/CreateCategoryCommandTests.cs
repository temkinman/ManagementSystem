using System.Linq.Expressions;
using AutoMapper;
using BuildingBlocks.Exceptions;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Interfaces;
using Catalog.Application.Mapping;
using Catalog.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Catalog.Tests.Commands;

public class CreateCategoryCommandTests
{
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandTests()
    {
        _mockRepo = new Mock<ICategoryRepository>();
        
        var config = new MapperConfiguration(cfg => 
            cfg.AddProfile<CatalogMappingProfile>());
        _mapper = config.CreateMapper();

        _handler = new CreateCategoryCommandHandler(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ReturnsCategoryId_WhenCategoryIsValid()
    {
        // Arrange
        var command = new CreateCategoryCommand("Electronics");
        var expectedId = Guid.NewGuid();

        _mockRepo
            .Setup(r => r.GetItemByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        _mockRepo
            .Setup(r => r.CreateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Category { Id = expectedId, Name = command.CategoryName });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Id.Should().Be(expectedId);
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ThrowsConflictException_WhenCategoryExists()
    {
        // Arrange
        var command = new CreateCategoryCommand("Electronics");
        var existingCategory = new Category { Id = Guid.NewGuid(), Name = command.CategoryName };

        _mockRepo
            .Setup(r => r.GetItemByConditionAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCategory);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
}