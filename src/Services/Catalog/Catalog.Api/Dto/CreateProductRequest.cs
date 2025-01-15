using Catalog.Application.Dtos;

namespace Catalog.Api.Dto;

public record CreateProductRequest(ProductDto ProductDto);

public record CreateProductResponse(Guid Id);