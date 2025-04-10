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
    public IReadOnlyCollection<Tag> Tags => _tags;
    private readonly List<Tag> _tags = [];
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

    public static Article Create(string title, string content, List<Tag>? tags = null)
    {
        if (string.IsNullOrEmpty(title))
            throw new NullOrEmptyException(nameof(title));
        if (title.Length > TitleMaxLength)
            throw new ArgumentException($"Title is more than limit: {TitleMaxLength} characters");
        if (string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(content));
        var article = new Article(title, content, DateTime.Now);
        if (tags is not null && tags.Count != 0)
            article.AddTags(tags);
        return article;
    }

    public void Update(string? title = null, string? content = null, List<Tag>? tags = null)
    {
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(title) + "," + nameof(content));
        if (!string.IsNullOrEmpty(title))
            Title = title;
        if (!string.IsNullOrEmpty(content))
            Content = content;
        if (tags is not null && tags.Count != 0)
            AddTags(tags);
        UpdatedAt = DateTime.Now;
    }

    private void AddTags(List<Tag> tags) =>
        tags.ForEach(tag =>
        {
            tag.AddArticle(this);
            _tags.Add(tag);
        });
}
