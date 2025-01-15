using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.CreateProduct;

public record CreateProductCommand(ProductDto ProductDto) : ICommand<CreateProductResult>;

public record  CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductDto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ProductDto.Name).MaximumLength(100).WithMessage("ProductName's length must be maximum 100 symbols");
        RuleFor(x => x.ProductDto.Description).MaximumLength(800).WithMessage("Maximum description's length is 800 symbols");
        RuleFor(x => x.ProductDto.Quantity).GreaterThan(0).WithMessage("Quantity must be a positive number");
        RuleFor(x => x.ProductDto.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}