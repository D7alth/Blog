using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Configuration.Providers;

public sealed class SqlServerDbContextProvider(string connectionString) : IDbContextOptionsProvider
{
    public void Configure(DbContextOptionsBuilder optionsProvider)
    {
        optionsProvider.UseSqlServer(connectionString);
    }
}
