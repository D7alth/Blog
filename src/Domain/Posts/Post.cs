using Blog.Domain.Posts.ValueObjects;
using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts;

public sealed class Post : Entity<int>, IAggregateRoot
{
    public string? Title { get; }
    public string? Content { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public List<Tag> Tags { get; } = [];
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
}
