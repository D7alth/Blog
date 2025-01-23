using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts.Entities;

public sealed class Comment : Entity<int>
{
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; }
    public Guid AuthorId { get; private set; }
    public int PostId { get; private set; }
    public List<Comment> Comments { get; set; }

    private Comment()
        : base(default)
    {
        Comments = [];
    }

    private Comment(Guid authorId, int postId, string content)
        : base(default)
    {
        Content = content;
        CreatedAt = DateTime.Now;
        AuthorId = authorId;
        PostId = postId;
        Comments = [];
    }

    public static Comment Create(
        Guid authorId,
        int postId,
        string content,
        bool isValidPost,
        bool isValidAuthor
    ) =>
        !isValidAuthor
            ? throw new NotValidException(nameof(authorId))
            : !isValidPost
                ? throw new NotValidException(nameof(postId))
                : string.IsNullOrEmpty(content)
                    ? throw new NullOrEmptyException(nameof(content))
                    : new Comment(authorId, postId, content);

    public void Reply(Guid authorId, string content, bool isValidAuthor)
    {
        if (string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(content));
        if (!isValidAuthor)
            throw new NotValidException(nameof(authorId));
        Comments.Add(new Comment(authorId, this.PostId, content));
    }
}
