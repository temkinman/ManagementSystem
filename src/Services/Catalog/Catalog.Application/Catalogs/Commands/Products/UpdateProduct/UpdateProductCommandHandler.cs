using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public UpdateProductCommandHandler(IMapper mapper,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        Product productInput = _mapper.Map<Product>(command);

        Product? existingProduct = await _productRepository.GetItemByConditionAsync(x => x.Name.ToLower() == productInput.Name.ToLower(), cancellationToken);
        if (existingProduct != null && existingProduct.CategoryId == productInput.CategoryId)
        {
            throw new ConflictException("Product with this name for this category already exists.");
        }

        await InitCategoryForProduct(productInput, command.CategoryId, cancellationToken);
        
        Product updatedProduct = await _productRepository.UpdateAsync(productInput, cancellationToken);
        ProductDto updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);

        return new UpdateProductResult(updatedProductDto);
    }
    
    public async Task InitCategoryForProduct(Product product, Guid? categoryId,  CancellationToken cancellationToken)
    {
        product.Category = await _categoryRepository.GetItemByConditionAsync(
            x => x.Id == categoryId, cancellationToken);
    }
}