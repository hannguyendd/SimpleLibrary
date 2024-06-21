using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Infrastructure.Repositories;

public class AccountRepository(LibraryContext context) : GenericRepository<Account>(context), IAccountRepository
{
    public Task<Account?> GetByUsernameAsync(string username)
    {
        return GetQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
    }
}