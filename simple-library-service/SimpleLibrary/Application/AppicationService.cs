using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application;

public abstract class ApplicationService(IUnitOfWork uow)
{
    protected readonly IUnitOfWork _uow = uow;
}