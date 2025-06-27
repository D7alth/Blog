using Blog.PostContext.Domain.Articles.Entities;
using Blog.PostContext.Domain.Articles.Repositories;
using Blog.PostContext.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.PostContext.Infrastructure.Domain.Articles.Repository;

public sealed class CategoryRepository(ApplicationContext context) : ICategoryRepository
{
    public void Add(Category category) => context.Categories.Add(category);

    public async Task<Category?> GetCategoryById(int id) => await context.Categories.FindAsync(id);

    public Task<bool> ExistsAsync(string name) => context.Categories.AnyAsync(t => t.Name == name);
}
