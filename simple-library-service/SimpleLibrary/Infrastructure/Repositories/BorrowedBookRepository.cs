using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Infrastructure.Repositories;

public class BorrowedBookRepository : GenericRepository<BorrowedBook>, IBorrowedBookRepository
{
    public BorrowedBookRepository(LibraryContext context) : base(context)
    {
    }
}