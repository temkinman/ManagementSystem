using AutoMapper;
using Catalog.Api.Dto;
using Catalog.Application.Catalogs.Commands.Categories.CreateCategory;
using Catalog.Application.Catalogs.Commands.Categories.DeleteProduct;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Catalogs.Queries.Categories.GetAllCategories;
using Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryById;
using Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryByName;
using Catalog.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The request object containing the data for creating the category.</param>
        /// <returns>
        /// Returns the result of the category creation as a <see cref="CreateProductResponse"/> object with a status code of 200 (OK)
        /// if the product is successfully created. 
        /// In case of validation errors, it returns a status code of 400 (Bad Request).
        /// If a product with the same name already exists, it returns a status code of 409 (Conflict).
        /// In case of an unexpected error, it returns a status code of 500 (Internal Server Error).
        /// </returns>
        [HttpPost]
        [Route("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateCategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(request.CategoryName));
            var response = _mapper.Map<CreateCategoryResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// Updating a new category.
        /// </summary>
        /// <param name="request">The request object containing the data for updating the category.</param>
        /// <returns>
        /// Returns the result of the category updated as a <see cref="UpdateCategoryResponse"/> object with a status code of 200 (OK)
        /// if the category is successfully updated. 
        /// In case of validation errors, it returns a status code of 400 (Bad Request).
        /// If a category with the same name already exists, it returns a status code of 409 (Conflict).
        /// In case of an unexpected error, it returns a status code of 500 (Internal Server Error).
        /// </returns>
        [HttpPut]
        [Route("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateProductResponse>> UpdateCategory(UpdateCategoryRequest request)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<UpdateCategoryResponse>(result);

            return Ok(response);
        }

        /// <summary>
        ///  Deleting category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// Returns the result of the product deleted as a <see cref="DeleteCategoryResponse"/> object with a status code of 200 (OK)
        [HttpDelete]
        [Route("categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteCategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteCategoryResponse>> DeleteProduct(Guid categoryId)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(categoryId));
            var response = _mapper.Map<DeleteCategoryResponse>(result);

            return Ok(response);
        }
        
        /// <summary>
        ///  Getting category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Returns the result of the product as a <see cref="CategoryDto"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid categoryId)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
            var response = _mapper.Map<CategoryDto>(result.Category);

            return Ok(response);
        }
        
        /// <summary>
        ///  Getting all categories
        /// </summary>
        /// <returns></returns>
        /// Returns the result of the categories as a <see cref="IEnumerable<CategoryDto>"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var response = await _mediator.Send(new GetAllCategoriesQuery());

            return Ok(response.Categories);
        }

        /// <summary>
        ///  Getting category by name
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        /// Returns the result of the categories as a <see cref="CategoryDto"/> object with a status code of 200 (OK)
        [HttpGet]
        [Route("categories/search/{categoryName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> GetCategoryByName(string categoryName)
        {
            var result = await _mediator.Send(new GetCategoryByNameQuery(categoryName));
            var response = _mapper.Map<CategoryDto>(result.Category);

            return Ok(response);
        }
    }
}
