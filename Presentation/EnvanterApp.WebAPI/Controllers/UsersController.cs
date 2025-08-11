using EnvanterApp.Application.Features;
using EnvanterApp.Application.Features.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanterApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserQueryRequest loginUserQueryRequest)
        {
            GeneralResponse<LoginUserQueryResponse> response = await _mediator.Send(loginUserQueryRequest);

            if(!response.IsSuccess)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
