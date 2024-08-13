using System.Reflection;
using Bookworm.Application.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookworm.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(mediatrConfig => mediatrConfig.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

        return services;
    }
}