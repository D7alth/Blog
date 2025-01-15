using Blog.Domain.Entities.Posts.ValueObjects;

namespace Blog.Domain.Entities.Posts;

public sealed class Post 
{
    public int Id { get; init; }
    public string? Title { get; }
    public string? Content { get;  }
    public List<Tag> Tags { get; } = [];
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    
    private Post()
    { }

    private Post(string title, string content, List<Tag> tags, DateTime updatedAt)
    {
        Title = title;
        Content = content;
        Tags = tags;
        CreatedAt = DateTime.Now;
        UpdatedAt = updatedAt;
    }

    public static Post Create(string title, string content, List<Tag> tags)
        =>
            string.IsNullOrEmpty(title) ? throw new ArgumentException("Title cannot be null") :
            string.IsNullOrEmpty(content) ? throw new ArgumentException("Content cannot be null") :
            new Post(title, content, tags, DateTime.Now);
}