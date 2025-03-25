using Blog.Domain.Posts.ValueObjects;
using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts;

public sealed class Post : Entity<int>, IAggregateRoot
{
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
    public List<Tag> Tags { get; private set; } = [];
    private const int TitleMaxLength = 60;

    private Post()
        : base(default) { }

    private Post(string title, string content, DateTime updatedAt)
        : base(default)
    {
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
        UpdatedAt = updatedAt;
    }

    public static Post Create(string title, string content) =>
        string.IsNullOrEmpty(title)
            ? throw new NullOrEmptyException(nameof(title))
            : title.Length > TitleMaxLength
                ? throw new ArgumentException(
                    $"Title is more than limit: {TitleMaxLength} characters"
                )
                : string.IsNullOrEmpty(content)
                    ? throw new NullOrEmptyException(nameof(content))
                    : new Post(title, content, DateTime.Now);

    public void AddTag(string tag)
    {
        if (Tags.Any(t => t.Name == tag))
            return;
        Tags.Add(Tag.Create(tag));
    }

    public void Update(string? title = null, string? content = null)
    {
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(title) + "," + nameof(content));
        if (!string.IsNullOrEmpty(title))
            Title = title;
        if (!string.IsNullOrEmpty(content))
            Content = content;
        UpdatedAt = DateTime.Now;
    }
}
