using Microsoft.EntityFrameworkCore;

namespace Blog.AccountContext.Infrastructure.Configuration.Providers;

public sealed class SqlLiteDbContextProvider(string connectionString) : IDbContextOptionsProvider
{
    public void Configure(DbContextOptionsBuilder optionsProvider)
    {
        optionsProvider.UseSqlite(connectionString);
    }
}
