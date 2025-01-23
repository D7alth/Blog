using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts.Entities;

public sealed class Comment : Entity<int>
{
    public string? Author { get; private set; }
    public string? Content { get; private set; }
    
    private Comment()
        : base(default) { }

    private Comment(string author, string content) : base(default)
    {
        Author = author;
        Content = content;
    }

    public static Comment Create(string author, string content) =>
        string.IsNullOrEmpty(author) ? 
            throw new NullOrEmptyException(nameof(author)) :
        string.IsNullOrEmpty(content) ? 
            throw new NullOrEmptyException(nameof(content)) :
            new Comment(author, content);
}