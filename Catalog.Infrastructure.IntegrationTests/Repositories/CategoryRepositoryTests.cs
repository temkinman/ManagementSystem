using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Contexts;
using Catalog.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.IntegrationTests.Repositories;

public class CategoryRepositoryTests : IDisposable
{
    private readonly CatalogDbContext _dbContext;
    private readonly ICategoryRepository _repository;

    public CategoryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new CatalogDbContext(options);
        _repository = new CategoryRepository(_dbContext);
    }

    [Fact]
    public async Task CreateAsync_AddsCategoryToDatabase()
    {
        // Arrange
        var category = new Category { Name = "Test Category" };

        // Act
        var result = await _repository.CreateAsync(category, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        _dbContext.Categories.Should().Contain(c => c.Name == "Test Category");
    }

    public void Dispose() => _dbContext.Dispose();
}