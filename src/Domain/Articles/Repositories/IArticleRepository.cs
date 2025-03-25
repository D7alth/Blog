namespace Blog.Domain.Articles.Repositories;

public interface IArticleRepository
{
    public void Add(Article article);
    public Task<List<Article>> GetAll();
    public Task<Article> GetById(int id);
    public void Update(Article article);
    public void Remove(Article article);
}
