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

        Product? existingProduct = await _productRepository.GetItemByConditionAsync(x => x.Id == productInput.Id, cancellationToken);
        if (existingProduct == null)
        {
            throw new NotFoundException(nameof(Product),$"Product with this {productInput.Id} wasn't found.");
        }

        await InitCategoryForProduct(productInput, command.CategoryId, cancellationToken);
        
        productInput.CreatedDateUtc = existingProduct.CreatedDateUtc;
        
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