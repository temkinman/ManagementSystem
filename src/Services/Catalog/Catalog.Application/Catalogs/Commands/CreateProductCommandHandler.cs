using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Catalogs.Commands;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductCommand> _createProductValidator;
    
    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, IValidator<CreateProductCommand> createProductValidator)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _createProductValidator = createProductValidator;
    }
    
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        Product productInput = _mapper.Map<Product>(command);

        Product? existingProduct = await _productRepository.GetItemByConditionAsync(x => x.Name == productInput.Name,cancellationToken);
        if (existingProduct != null)
        {
            throw new ConflictException("Product with this name already exists.");
        }
        
        productInput.Id = Guid.NewGuid();
        productInput.CreatedDateUtc = DateTime.UtcNow;
        productInput.UpdatedDateUtc = DateTime.UtcNow;
        productInput.CategoryId = Guid.Parse("ab7b3311-1ae1-4bd8-911b-c1f4538b814d");
        
        Product addedProduct = await _productRepository.CreateAsync(productInput, cancellationToken);

        return new CreateProductResult(addedProduct.Id);
    }
}