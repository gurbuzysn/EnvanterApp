using EnvanterApp.Application.Features.Commands.Categories;
using EnvanterApp.Application.Features.Commands.Categories.RemoveCategory;
using EnvanterApp.Application.Features.Commands.Categories.UpdateCategory;
using EnvanterApp.Application.Features.Commands.Employees.UpdateEmployee;
using EnvanterApp.Application.Features.Queries.Categories.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanterApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryCommandRequest addCategoryCommandRequest)
        {
            var result = await _mediator.Send(addCategoryCommandRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQueryRequest());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RemoveCategoryCommandRequest() {Id = id});
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            updateCategoryCommandRequest.Id = id;
             var result = await _mediator.Send(updateCategoryCommandRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
