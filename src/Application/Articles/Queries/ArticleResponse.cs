using Blog.PostContext.Domain.Articles.Entities;

namespace Blog.PostContext.Application.Articles.Queries;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    Category Category,
    DateTime CratedAt,
    DateTime UpdatedAt
);
