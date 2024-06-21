using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Infrastructure.Repositories;

public class UnitOfWork(LibraryContext context) : IUnitOfWork
{
    public Task CompleteAsync()
    {
        return context.SaveChangesAsync();
    }
}