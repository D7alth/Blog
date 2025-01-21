using Blog.Domain.Entities.Posts.ValueObjects;

namespace Blog.Domain.Entities.Posts;

public sealed class Post : Entity<int>
{
    public string? Title { get; }

    // TODO: Using a Markdown editor to content management
    public string? Content { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public List<Tag> Tags { get; } = [];
    const int TitleMaxLength = 60;

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
            ? throw new ArgumentException("Title cannot be null")
            : title.Length > TitleMaxLength
                ? throw new ArgumentException(
                    $"Title is more than limit: {TitleMaxLength} characters"
                )
                : string.IsNullOrEmpty(content)
                    ? throw new ArgumentException("Content cannot be null")
                    : new Post(title, content, DateTime.Now);

    public void AddTag(string tag)
    {
        if (Tags.Any(t => t.Name == tag))
            return;
        Tags.Add(Tag.Create(tag));
    }
}
