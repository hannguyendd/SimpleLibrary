using MediatR;
using SimpleLibrary.Application.Contracts.Dtos.Authentication;

namespace SimpleLibrary.Application.Contracts.Commands.Authentication;

public record LoginCommand(LoginRequest Credentials) : IRequest<TokenDto>;