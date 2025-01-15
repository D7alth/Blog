using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Configuration;

public interface IDbContextOptionsProvider
{
    void Configure(DbContextOptionsBuilder optionsProvider);
}