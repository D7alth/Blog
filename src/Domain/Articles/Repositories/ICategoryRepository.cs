using Blog.Domain.Articles.Entities;

namespace Blog.Domain.Articles.Repositories;

public interface ICategoryRepository
{
    public void Add(Category category);
    public Task<Category?> GetCategoryById(int id);
    public Task<bool> ExistsAsync(string name);
}
