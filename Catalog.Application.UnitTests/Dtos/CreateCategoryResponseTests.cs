using Catalog.Api.Dto;

namespace Catalog.Tests.Dtos;

public class CreateCategoryResponseTests
{
    [Fact]
    public void CreateCategoryResponse_ShouldHaveCorrectId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new CreateCategoryResponse(id);

        // Assert
        Assert.Equal(id, response.Id);
    }
}