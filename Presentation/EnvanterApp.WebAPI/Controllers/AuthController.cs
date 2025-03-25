using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvanterApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public async Task<IActionResult> Login()
        {


            return Ok();
        }
    }
}
