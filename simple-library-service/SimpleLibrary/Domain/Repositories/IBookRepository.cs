using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Domain.Repositories;

public interface IBookRepository
{
    IQueryable<Book> GetQueryable();
    Task<IEnumerable<Book>> GetAllAsync();
}