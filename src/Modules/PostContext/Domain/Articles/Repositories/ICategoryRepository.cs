using Blog.PostContext.Domain.Articles.Entities;

namespace Blog.PostContext.Domain.Articles.Repositories;

public interface ICategoryRepository
{
    public void Add(Category category);
    public Task<Category?> GetCategoryById(int id);
    public Task<List<Category>> GetCategoriesAsync();
    public Task<bool> ExistsAsync(string name);
}
