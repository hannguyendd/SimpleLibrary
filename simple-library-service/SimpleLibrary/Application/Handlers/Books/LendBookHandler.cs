using MapsterMapper;
using MediatR;
using SimpleLibrary.Application.Contracts.Commands.Books;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Application.Contracts.Managers;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Application.Handlers.Base;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application.Handlers.Books;

public class LendBooHandler(IUnitOfWork uow, IMapper mapper, IBookManager bookManager, ICurrentUserService currentUserService) : BaseHandler(uow, mapper), IRequestHandler<LendBookCommand, BorrowedBookDto>
{
    private readonly IBookManager _bookManager = bookManager;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<BorrowedBookDto> Handle(LendBookCommand request, CancellationToken cancellationToken)
    {
        var borrowedBook = await _bookManager.BorrowBookAsync(new CreateBorrowedBookDto(_currentUserService.UserId, request.BookId));

        await _uow.CompleteAsync();
        
        return _mapper.Map<BorrowedBookDto>(borrowedBook);
    }
}