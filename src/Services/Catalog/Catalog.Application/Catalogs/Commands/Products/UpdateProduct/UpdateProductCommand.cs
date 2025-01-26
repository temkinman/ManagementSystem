using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    Guid? CategoryId) : ICommand<UpdateProductResult>;

public record UpdateProductResult(ProductDto Product);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("ProductName's length must be maximum 100 symbols");
        RuleFor(x => x.Description).MaximumLength(800).WithMessage("Maximum description's length is 800 symbols");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be a positive number");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}