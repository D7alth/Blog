namespace Blog.Domain.Entities.Posts;

public sealed class Post
{
    public int Id { get; init; }
    public string? Title { get; }

    // TODO: Using a Markdown editor to content management
    public string? Content { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    const int TitleMaxLength = 60;

    private Post() { }

    private Post(string title, string content, DateTime updatedAt)
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
}
