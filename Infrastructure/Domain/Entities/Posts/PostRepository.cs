using Blog.Domain.Entities.Posts;
using Blog.Domain.Entities.Posts.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Entities.Posts;

public sealed class PostRepository(ApplicationContext context) : IPostRepository
{
    public void Add(Post post)
    {
        context.Posts.Add(post);
    }

    public async Task<List<Post>> GetAll() => await context.Posts.ToListAsync();

    public async Task<Post> GetById(int id) =>
        await context.Posts.FindAsync(id) ?? throw new KeyNotFoundException($"Post not found");

    public void Update(Post post)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        var post = context.Posts.Find(id) ?? throw new KeyNotFoundException($"Post not found");
        context.Remove(post);
    }
}