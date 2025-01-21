namespace Catalog.Api.Dto;

public record CreateCategoryRequest(string CategoryName);

public record CreateCategoryResult(Guid CategoryId);