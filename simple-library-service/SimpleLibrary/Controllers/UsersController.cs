using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Application.Contracts.Commands.Books;

namespace SimpleLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IMediator mediator) : AppBaseController(mediator)
{

    [HttpGet("me/borrowed-books")]
    public async Task<IActionResult> GetBorrowedBooks()
    {
        var result = await _mediator.Send(new GetUserBorrowedBookCommand());

        return Ok(result);
    }
}