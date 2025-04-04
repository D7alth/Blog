using Blog.Domain.Shared;

namespace Blog.Domain.Articles.Entities;

public sealed class Tag : Entity<int>
{
    public string? Name { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public IReadOnlyCollection<Article> Articles => _articles;
    private readonly List<Article> _articles = [];

    private Tag()
        : base(default) { }

    private Tag(string name)
        : base(default)
    {
        Name = name;
        Slug = name.ToLower().Replace(" ", "-");
    }

    public static Tag Create(string name, bool isDuplicate) =>
        new(isDuplicate ? throw new ArgumentException("Duplicate tag") : name);

    public void AddArticle(Article article) => _articles.Add(article);
}
