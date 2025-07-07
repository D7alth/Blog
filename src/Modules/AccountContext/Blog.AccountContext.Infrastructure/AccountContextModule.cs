using Blog.AccountContext.Infrastructure.Configuration;
using Blog.AccountContext.Infrastructure.Configuration.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.AccountContext.Infrastructure;

public static class AccountContextModule
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IDbContextOptionsProvider>(provider => new SqlLiteDbContextProvider(
            "DataSource = identityDb; Cache=Shared"
        ));
    }
}