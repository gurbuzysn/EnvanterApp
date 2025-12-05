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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQueryRequest loginUserQueryRequest)
        {
            var result = await _mediator.Send(loginUserQueryRequest);
            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
    }
}
