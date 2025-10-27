using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Features.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanterApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
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
