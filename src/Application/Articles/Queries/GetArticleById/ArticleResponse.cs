using Blog.Domain.Articles.ValueObjects;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    string Title,
    string Content,
    IEnumerable<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
