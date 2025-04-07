using Blog.Domain.Articles.Entities;

namespace Blog.Domain.Articles.Repositories;

public interface ITagRepository
{
    public void Add(Tag tag);
    public Task<bool> ExistsAsync(string name);
}
