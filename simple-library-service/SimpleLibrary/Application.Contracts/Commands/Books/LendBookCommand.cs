using MediatR;
using SimpleLibrary.Application.Contracts.Dtos.Books;

namespace SimpleLibrary.Application.Contracts.Commands.Books;

public record LendBookCommand(Guid BookId) : IRequest<BorrowedBookDto>;