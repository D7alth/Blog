using Blog.Domain.Articles.Entities;

namespace Blog.Application.Articles.Queries;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    IEnumerable<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
