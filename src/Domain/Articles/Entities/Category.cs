using Blog.Domain.Shared;

namespace Blog.Domain.Articles.Entities;

public sealed class Category : Entity<int>
{
    public string? Name { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public IReadOnlyCollection<Article> Articles => _articles;
    private readonly List<Article> _articles = [];

    private Category()
        : base(default) { }

    private Category(string name)
        : base(default)
    {
        Name = name;
        Slug = name.ToLower().Replace(" ", "-");
    }

    public static Category Create(string name, bool isDuplicated) =>
        new(isDuplicated ? throw new ArgumentException("Duplicate category") : name);

    public void AddArticle(Article article) => _articles.Add(article);
}
