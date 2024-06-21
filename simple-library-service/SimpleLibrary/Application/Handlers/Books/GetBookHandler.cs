using MapsterMapper;
using MediatR;
using SimpleLibrary.Application.Contracts.Commands.Books;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Application.Handlers.Base;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application.Handlers.Books;

public class GetBookHandler(IUnitOfWork uow, IMapper mapper, IBookRepository bookRepository) : BaseHandler(uow, mapper), IRequestHandler<GetBookCommand, ICollection<BookDto>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<ICollection<BookDto>> Handle(GetBookCommand request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();
        
        return _mapper.Map<ICollection<BookDto>>(books);
    }
}
