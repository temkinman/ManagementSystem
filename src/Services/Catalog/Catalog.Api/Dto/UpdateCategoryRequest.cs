using Catalog.Application.Dtos;

namespace Catalog.Api.Dto;

public record UpdateCategoryRequest(
    Guid Id,
    string Name);

public record UpdateCategoryResponse(CategoryDto Category);