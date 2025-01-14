using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CatalogController(ILogger<CatalogController> logger, IMediator mediator, IMapper mapper)
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
    }
}
