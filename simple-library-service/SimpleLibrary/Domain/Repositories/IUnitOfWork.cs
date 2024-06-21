namespace SimpleLibrary.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}