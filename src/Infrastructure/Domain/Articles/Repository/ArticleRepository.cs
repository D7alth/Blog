using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Articles.Repository;

public sealed class ArticleRepository(ApplicationContext context) : IArticleRepository
{
    public void Add(Article article) => context.Articles.Add(article);

    public async Task<List<Article>> GetArticlesAsync(
        DateTime? startDate,
        DateTime? endDate,
        int limit,
        int page
    )
    {
        if (startDate.HasValue)
            context.Articles.Where(a => a.CreatedAt >= startDate);
        if (endDate.HasValue)
            context.Articles.Where(a => a.CreatedAt <= endDate);
        return await context.Articles.Skip((page - 1) * limit).Take(limit).ToListAsync();
    }

    public Task<bool> ExistsAsync(int id) => context.Articles.AnyAsync(a => a.Id == id);

    public async Task<Article> GetById(int id) => await context.Articles.FindAsync(id) ?? null!;

    public void Update(Article article) => context.Articles.Update(article);

    public void Remove(Article article) => context.Remove(article);
}
