using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleLibrary.Controllers;

public abstract class AppBaseController(IMediator mediator) : ControllerBase {
    protected readonly IMediator _mediator = mediator;
}