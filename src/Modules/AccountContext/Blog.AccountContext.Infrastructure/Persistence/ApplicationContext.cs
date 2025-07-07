using Blog.AccountContext.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.AccountContext.Infrastructure.Persistence;

public class ApplicationContext(IDbContextOptionsProvider dbContextOptionsProvider) : IdentityDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            dbContextOptionsProvider.Configure(optionsBuilder);
    }
}
