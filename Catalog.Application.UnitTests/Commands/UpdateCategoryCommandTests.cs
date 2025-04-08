using System.Linq.Expressions;
using AutoMapper;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Interfaces;
using Catalog.Application.Mapping;
using Catalog.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Catalog.Tests.Commands;

public class UpdateCategoryCommandTests
{
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly UpdateCategoryCommandHandler _commandHandler;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandTests()
    {
        _mockRepo = new Mock<ICategoryRepository>();
        var config = new MapperConfiguration(cfg => 
            cfg.AddProfile<CatalogMappingProfile>());
        _mapper = config.CreateMapper();
        _commandHandler = new UpdateCategoryCommandHandler(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task Handle_UpdatesCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateCategoryCommand(categoryId, "New Name");
        var existingCategory = new Category{ Id = categoryId, Name = "Old Name"};
        var expectedCategory = new Category{ Id = categoryId, Name = command.Name };
        
        // Мок для проверки существования категории (по ID)
        _mockRepo
            .Setup(r => r.GetItemByConditionAsync(
                It.Is<Func<Category, bool>(x => x.Id == expectedCategory.Id),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCategory);
        
        _mockRepo
            .Setup(r => r.GetItemByConditionAsync(
                It.IsAny<Expression<Func<Category, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category)null);
        
        _mockRepo
            .Setup(r => r.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategory);
        
        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Category.Name.Should().Be(expectedCategory.Name);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}