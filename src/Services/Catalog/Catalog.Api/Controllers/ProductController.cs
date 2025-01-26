using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.Products.CreateProduct;
using Catalog.Application.Catalogs.Commands.Products.DeleteProduct;
using Catalog.Application.Catalogs.Commands.Products.UpdateProduct;
using Catalog.Application.Catalogs.Queries.GetAllProducts;
using Catalog.Application.Catalogs.Queries.GetProductById;
using Catalog.Application.Catalogs.Queries.GetProductsByCategoryName;
using Catalog.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The request object containing the data for creating the product.</param>
        /// <returns>
        /// Returns the result of the product creation as a <see cref="CreateProductResponse"/> object with a status code of 200 (OK)
        /// if the product is successfully created. 
        /// In case of validation errors, it returns a status code of 400 (Bad Request).
        /// If a product with the same name already exists, it returns a status code of 409 (Conflict).
        /// In case of an unexpected error, it returns a status code of 500 (Internal Server Error).
        /// </returns>
        [HttpPost]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductRequest request)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateProductResponse>(result);

            return Ok(response);
        }
        
        /// <summary>
        /// Updating a new product.
        /// </summary>
        /// <param name="request">The request object containing the data for updating the product.</param>
        /// <returns>
        /// Returns the result of the product updated as a <see cref="UpdateProductResponse"/> object with a status code of 200 (OK)
        /// if the product is successfully updated. 
        /// In case of validation errors, it returns a status code of 400 (Bad Request).
        /// If a product with the same name already exists for such category, it returns a status code of 409 (Conflict).
        /// In case of an unexpected error, it returns a status code of 500 (Internal Server Error).
        /// </returns>
        [HttpPut]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(UpdateProductRequest request)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<UpdateProductResponse>(result);

            return Ok(response);
        }
        
        /// <summary>
        ///  Deleting product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// Returns the result of the product deleted as a <see cref="DeleteProductResponse"/> object with a status code of 200 (OK)
        [HttpDelete]
        [Route("products/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteProductResponse>> DeleteProduct(Guid productId)
        {
            var result = await _mediator.Send(new DeleteProductCommand(productId));
            var response = _mapper.Map<DeleteProductResponse>(result);

            return Ok(response);
        }
        
        /// <summary>
        ///  Getting product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Returns the result of the product as a <see cref="GetProductByIdResponse"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductByIdResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetProductByIdResponse>> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            var response = _mapper.Map<GetProductByIdResponse>(result);

            return Ok(response);
        }
        
        /// <summary>
        ///  Getting all products
        /// </summary>
        /// <returns></returns>
        /// Returns the result of the product as a <see cref="IEnumerable<ProductDto>"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllProductsQuery());

            return Ok(response.Products);
        }
        
        /// <summary>
        ///  Getting products by category
        /// </summary>
        /// <returns></returns>
        /// Returns the result of the product as a <see cref="IEnumerable<ProductDto>"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("{categoryName}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string categoryName)
        {
            var result = await _mediator.Send(new GetProductsByCategoryNameQuery(categoryName));
            var response = _mapper.Map<IEnumerable<ProductDto>>(result.Products);

            return Ok(response);
        }
    }
}
