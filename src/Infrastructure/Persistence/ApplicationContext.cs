using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;
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

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}
