using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Domain.Articles.Repository;

public sealed class ArticleRepository(ApplicationContext context) : IArticleRepository
{
    public void Add(Article article) => context.Article.Add(article);

    public async Task<List<Article>> GetAll() => await context.Article.ToListAsync();

    public async Task<Article> GetById(int id) => await context.Article.FindAsync(id) ?? null!;

    public void Update(Article article) => context.Article.Update(article);

    public void Remove(Article article) => context.Remove(article);
}
