using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Articles.Entities;

public sealed class Comment : Entity<int>
{
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; }
    public Guid AuthorId { get; private set; }
    public int ArticleId { get; private set; }
    public IReadOnlyCollection<Comment> Comments => _comments;
    private readonly List<Comment> _comments = [];

    private Comment()
        : base(default) { }

    private Comment(Guid authorId, int articleId, string content)
        : base(default)
    {
        Content = content;
        CreatedAt = DateTime.Now;
        AuthorId = authorId;
        ArticleId = articleId;
        _comments = [];
    }

    public static Comment Create(
        Guid authorId,
        int articleId,
        string content,
        bool isValidArticle,
        bool isValidAuthor
    ) =>
        !isValidAuthor
            ? throw new NotValidException(nameof(authorId))
            : !isValidArticle
                ? throw new NotValidException(nameof(articleId))
                : string.IsNullOrEmpty(content)
                    ? throw new NullOrEmptyException(nameof(content))
                    : new Comment(authorId, articleId, content);

    public void Reply(Guid authorId, string content, bool isValidAuthor)
    {
        if (string.IsNullOrEmpty(content))
            throw new NullOrEmptyException(nameof(content));
        if (!isValidAuthor)
            throw new NotValidException(nameof(authorId));
        _comments.Add(new Comment(authorId, ArticleId, content));
    }
}
