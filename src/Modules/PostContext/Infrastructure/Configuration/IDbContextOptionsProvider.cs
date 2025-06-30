using Microsoft.EntityFrameworkCore;

namespace Blog.PostContext.Infrastructure.Configuration;

public interface IDbContextOptionsProvider
{
    void Configure(DbContextOptionsBuilder optionsProvider);
}
