using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? existingProduct = await _productRepository.GetItemByConditionAsync(x => x.Id == command.ProductId, cancellationToken);
        
        if (existingProduct == null)
        {
            return new DeleteProductResult(false);
        }
        
        await _productRepository.DeleteAsync(existingProduct, cancellationToken);

        return new DeleteProductResult(true);
    }
}
