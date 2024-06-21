using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Domain.Repositories;

public interface IAccountRepository
{
    IQueryable<Account> GetQueryable();
    Task<Account?> GetByUsernameAsync(string username);
}