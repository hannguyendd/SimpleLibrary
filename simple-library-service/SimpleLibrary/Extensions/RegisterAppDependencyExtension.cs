using SimpleLibrary.Application;
using SimpleLibrary.Application.Contracts.Managers;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Application.Managers;
using SimpleLibrary.Application.Services;
using SimpleLibrary.Domain.Repositories;
using SimpleLibrary.Infrastructure.Repositories;

namespace SimpleLibrary.Extensions;

public static class RegisterAppDependencyExtension
{
    public static void RegisterAppDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
        services.AddScoped<ISystemConfigurationRepository, SystemConfigurationRepository>();
        
        services.AddScoped<IBookManager, BookManager>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
    }
}