using Blog.Domain.Articles.Entities;
using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Articles;

public sealed class Article : Entity<int>, IAggregateRoot
{
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    private const int TitleMaxLength = 60;

    private Article()
        : base(default) { }

    private Article(string title, string content, DateTime updatedAt)
        : base(default)
    {
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
        UpdatedAt = updatedAt;
    }

    public static Article Create(string title, string content, Category? category = null)
    {
        if (string.IsNullOrEmpty(title))
            throw new NullOrEmptyException(nameof(title));
        if (title.Length > TitleMaxLength)
            throw new ArgumentException($"Title is more than limit: {TitleMaxLength} characters");
        if (string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(content));
        var article = new Article(title, content, DateTime.Now);
        if (category is not null)
            article.AddCategory(category);
        return article;
    }

    public void Update(string? title = null, string? content = null, Category? category = null)
    {
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(title) + "," + nameof(content));
        if (!string.IsNullOrEmpty(title))
            Title = title;
        if (!string.IsNullOrEmpty(content))
            Content = content;
        if (category is not null)
            AddCategory(category);
        UpdatedAt = DateTime.Now;
    }

    private void AddCategory(Category category)
    {
        category.AddArticle(this);
        Category = category;
    }
}
