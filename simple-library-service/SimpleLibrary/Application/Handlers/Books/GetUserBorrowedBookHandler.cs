using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Application.Contracts.Commands.Books;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Application.Handlers.Base;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application.Handlers.Books;

public class GetUserBorrowedBookHandler(IUnitOfWork uow, IMapper mapper, IBorrowedBookRepository borrowedBookRepository, ICurrentUserService currentUserService) : BaseHandler(uow, mapper), IRequestHandler<GetUserBorrowedBookCommand, ICollection<BorrowedBookDto>>
{
    private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ICollection<BorrowedBookDto>> Handle(GetUserBorrowedBookCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var borrowedBooks = await _borrowedBookRepository.GetQueryable()
            .AsNoTracking()
            .Include(x => x.Book)
            .Where(x => x.AccountId == userId)
            .ToArrayAsync();

        return _mapper.Map<ICollection<BorrowedBookDto>>(borrowedBooks);
    }
}
