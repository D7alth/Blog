using Blog.Domain.Articles.ValueObjects;

namespace Blog.Application.Articles.Queries.GetArticlesByTag;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    IEnumerable<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
