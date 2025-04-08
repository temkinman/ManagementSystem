using Catalog.Api.Dto;

namespace Catalog.Tests.Dtos;

public class CreateCategoryRequestTests
{
    [Fact]
    public void CreateCategoryRequest_ShouldHaveCorrectProperties()
    {
        // Arrange
        var request = new CreateCategoryRequest("Electronics");

        // Assert
        Assert.Equal("Electronics", request.CategoryName);
    }
}