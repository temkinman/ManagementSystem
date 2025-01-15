using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
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

        if (command.ProductDto.CategoryDto != null)
        {
            await InitCategoryForProduct(productInput, command.ProductDto.CategoryDto, cancellationToken);
        }
        
        Product addedProduct = await _productRepository.CreateAsync(productInput, cancellationToken);

        return new CreateProductResult(addedProduct.Id);
    }

    private async Task InitCategoryForProduct(Product product, CategoryDto categoryDto,  CancellationToken cancellationToken)
    {
        Category? existingCategory = await _categoryRepository.GetItemByConditionAsync(
            x => x.Name == categoryDto.Name, cancellationToken);
        
        if (existingCategory != null)
        {
            product.CategoryId = existingCategory.Id;
        }
        else
        {
            product.Category = new Category
            {
                Name = categoryDto.Name 
            };
        }
    }
}