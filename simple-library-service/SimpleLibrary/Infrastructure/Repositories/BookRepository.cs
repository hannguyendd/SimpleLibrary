using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context)
    {
    }
}