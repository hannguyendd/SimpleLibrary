using MapsterMapper;
using MediatR;
using SimpleLibrary.Application.Contracts.Commands.Books;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Application.Contracts.Managers;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Application.Handlers.Base;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application.Handlers.Books;

public class ReturnBookHandler(IUnitOfWork uow, IMapper mapper, IBookManager bookManager, ICurrentUserService currentUserService) : BaseHandler(uow, mapper), IRequestHandler<ReturnBookCommand, BorrowedBookDto>
{
    private readonly IBookManager _bookManager = bookManager;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<BorrowedBookDto> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var borrowedBook = await _bookManager.ReturnBookAsync(new ReturnBookDto(userId, request.BookId));

        await _uow.CompleteAsync();

        return _mapper.Map<BorrowedBookDto>(borrowedBook);
    }
}
