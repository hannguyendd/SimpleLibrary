using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Infrastructure.Repositories;

public class SystemConfigurationRepository(LibraryContext context) : GenericRepository<SystemConfiguration>(context), ISystemConfigurationRepository
{
    public Task<SystemConfiguration?> GetOneAsync()
    {
        return GetQueryable().AsNoTracking().FirstOrDefaultAsync();
    }
}
