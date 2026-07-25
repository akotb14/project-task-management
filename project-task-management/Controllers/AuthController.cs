using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using project_task_management.Application.Features.Authentication.Commands.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace project_task_management.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest registerCommandRquest)
        {
            
            return Ok(await _mediator.Send(registerCommandRquest));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest loginCommandRequest)
        {

            return Ok(await _mediator.Send(loginCommandRequest));
        }
    }
}
