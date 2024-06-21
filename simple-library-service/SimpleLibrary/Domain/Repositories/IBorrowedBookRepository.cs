using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Domain.Repositories;

public interface IBorrowedBookRepository
{
    IQueryable<BorrowedBook> GetQueryable();
    void Add(BorrowedBook borrowedBook);
}