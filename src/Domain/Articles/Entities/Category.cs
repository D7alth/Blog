using Blog.Domain.Shared;

namespace Blog.Domain.Articles.Entities;

public sealed class Category : Entity<int>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public IReadOnlyCollection<Article> Articles => _articles;
    private readonly List<Article> _articles = [];

    private Category()
        : base(default) { }

    private Category(string name, string description)
        : base(default)
    {
        Name = name;
        Description = description;
        Slug = name.ToLower().Replace(" ", "-");
    }

    public static Category Create(string name, string description, bool isDuplicated) =>
        new(isDuplicated ? throw new ArgumentException("Duplicate category") : name, description);

    public void AddArticle(Article article) => _articles.Add(article);
}
