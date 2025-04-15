using Blog.Domain.Articles.Entities;

namespace Blog.Application.Articles.Queries;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    Category Category,
    DateTime CratedAt,
    DateTime UpdatedAt
);
