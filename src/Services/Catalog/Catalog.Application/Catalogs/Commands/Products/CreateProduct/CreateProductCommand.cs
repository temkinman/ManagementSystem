using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.Products.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

public record  CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.Name).MaximumLength(100).WithMessage("ProductName's length must be maximum 100 symbols");
        RuleFor(x => x.Product.Description).MaximumLength(800).WithMessage("Maximum description's length is 800 symbols");
        RuleFor(x => x.Product.Quantity).GreaterThan(0).WithMessage("Quantity must be a positive number");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}