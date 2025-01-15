namespace Blog.Domain.Entities.Posts;

public sealed class Post
{
    public int Id { get; init; }
    public string? Title { get; }
    public string? Content { get; }

    //    public List<Tag> Tags { get; } = [];
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

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
            : string.IsNullOrEmpty(content)
                ? throw new ArgumentException("Content cannot be null")
                : new Post(title, content, DateTime.Now);
}
