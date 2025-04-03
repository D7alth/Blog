namespace Blog.Domain.Articles.Repositories;

public interface IArticleRepository
{
    public void Add(Article article);
    public Task<List<Article>> GetArticlesAsync(
        DateTime? startDate,
        DateTime? endDate,
        int limit,
        int page
    );
    public Task<bool> ExistsAsync(int id);
    public Task<Article> GetById(int id);
    public Task<List<Article>> GetArticlesByTagAsync(string tag, int limit, int page);
    public void Update(Article article);
    public void Remove(Article article);
}
