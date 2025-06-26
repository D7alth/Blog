using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Articles.Repository;

public sealed class CategoryRepository(ApplicationContext context) : ICategoryRepository
{
    public void Add(Category category) => context.Categories.Add(category);

    public async Task<Category?> GetCategoryById(int id) =>
        await context
            .Categories.Include(c => c.Articles)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

    public async Task<List<Category>> GetCategoriesAsync()
    => await context.Categories.ToListAsync();
    
    public Task<bool> ExistsAsync(string name) => context.Categories.AnyAsync(t => t.Name == name);
}
