using EnvanterApp.Application.Features.Commands.Categories;
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
    }
}
