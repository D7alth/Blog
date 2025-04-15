using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Articles.Repository;

public sealed class CategoryRepository(ApplicationContext context) : ICategoryRepository
{
    public void Add(Category category) => context.Categories.Add(category);

    public Task<bool> ExistsAsync(string name) => context.Categories.AnyAsync(t => t.Name == name);
}
