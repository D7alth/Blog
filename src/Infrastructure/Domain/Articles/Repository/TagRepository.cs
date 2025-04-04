using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Articles.Repository;

public sealed class TagRepository(ApplicationContext context) : ITagRepository
{
    public void Add(Tag tag) => context.Tags.Add(tag);

    public Task<bool> ExistsAsync(string name) => context.Tags.AnyAsync(t => t.Name == name);
}
