using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using FluentAssertions;

namespace Catalog.Tests.Validators;

public class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator = new();

    [Theory]
    [InlineData("", false)] // Empty
    [InlineData("A", true)] // Valid
    [InlineData(null, false)] // Null
    [InlineData("Very long category name that exceeds the maximum allowed length of 100 characters....*************.....", false)] // Too long
    public void Validate_CategoryName_ReturnsCorrectResult(string name, bool isValid)
    {
        // Arrange
        var command = new CreateCategoryCommand(name);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().Be(isValid);
    }
}