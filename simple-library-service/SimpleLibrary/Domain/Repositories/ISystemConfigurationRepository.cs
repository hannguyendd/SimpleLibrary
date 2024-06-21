using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Domain.Repositories;

public interface ISystemConfigurationRepository
{
    Task<SystemConfiguration?> GetOneAsync();
    void Update(SystemConfiguration systemConfiguration);
}