using Blog.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Providers;

public sealed class SqlServerDbContextProvider(string connectionString) : IDbContextOptionsProvider
{
    public void Configure(DbContextOptionsBuilder optionsProvider)
    {
        optionsProvider.UseSqlServer(connectionString);
    }
}