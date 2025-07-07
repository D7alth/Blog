using Microsoft.EntityFrameworkCore;

namespace Blog.AccountContext.Infrastructure.Configuration;

public interface IDbContextOptionsProvider
{
    void Configure(DbContextOptionsBuilder optionsProvider);
}
