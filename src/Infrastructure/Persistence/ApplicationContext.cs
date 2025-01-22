using Blog.Domain.Post;
using Blog.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence;

public class ApplicationContext(IDbContextOptionsProvider dbContextOptionsProvider) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            dbContextOptionsProvider.Configure(optionsBuilder);
    }

    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}
