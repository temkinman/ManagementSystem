using Catalog.Application.Dtos;

namespace Catalog.Api.Dto;

public record CreateProductRequest(ProductDto Product);

public record CreateProductResponse(Guid Id);