using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Application.Contracts.Commands.Books;

namespace SimpleLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IMediator mediator) : AppBaseController(mediator)
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var books = await _mediator.Send(new GetBookCommand());

        return Ok(books);
    }

    [Authorize]
    [HttpPost("{id}/lend")]
    public async Task<IActionResult> Lend([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new LendBookCommand(id));

        return Ok(result);
    }

    [Authorize]
    [HttpPost("{id}/return")]
    public async Task<IActionResult> Return([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new ReturnBookCommand(id));

        return Ok(result);
    }



}