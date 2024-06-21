using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Application.Contracts.Commands.Authentication;
using SimpleLibrary.Application.Contracts.Dtos.Authentication;

namespace SimpleLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest credentials)
    {
        var tokenResult = await _mediator.Send(new LoginCommand(credentials));

        return Ok(tokenResult);
    }
}