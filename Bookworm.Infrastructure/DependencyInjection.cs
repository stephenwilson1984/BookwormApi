using Bookworm.Application.Common.Interfaces;
using Bookworm.Infrastructure.Persistence;
using Bookworm.Infrastructure.Persistence.Interceptors;
using Bookworm.Infrastructure.Persistence.Repositories;
using Bookworm.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookworm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<BookwormContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(BookwormContext).Assembly.FullName)));

        services.AddScoped<IBookwormContext>(provider => provider.GetRequiredService<BookwormContext>());
        services.AddScoped<BookwormDbContextInitialiser>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}