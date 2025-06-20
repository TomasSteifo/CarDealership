using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarDealership.Application.Features.Authentication;
using System.Threading.Tasks;

namespace CarDealership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(new { token = result.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.Success)
                return Unauthorized(result.Errors);

            return Ok(new { token = result.Token });
        }
    }
}
