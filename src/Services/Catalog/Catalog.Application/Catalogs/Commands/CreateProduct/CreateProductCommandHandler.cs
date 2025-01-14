using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CreateProductCommandHandler(IMapper mapper,
        IProductRepository productRepository,
        IValidator<CreateProductCommand> createProductValidator,
        ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
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
        
        Category? existingCategory = null;
        if (command.ProductDto.CategoryDto != null)
        {
            existingCategory = await _categoryRepository.GetItemByConditionAsync(
                x => x.Name == command.ProductDto.CategoryDto.Name, cancellationToken);
        }

        if (existingCategory != null)
        {
            productInput.CategoryId = existingCategory.Id;
        }
        else if (command.ProductDto.CategoryDto != null)
        {
            productInput.Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = command.ProductDto.CategoryDto.Name 
            };
        }
        
        productInput.Id = Guid.NewGuid();
        productInput.CreatedDateUtc = DateTime.UtcNow;
        productInput.UpdatedDateUtc = DateTime.UtcNow;
        
        Product addedProduct = await _productRepository.CreateAsync(productInput, cancellationToken);

        return new CreateProductResult(addedProduct.Id);
    }
}