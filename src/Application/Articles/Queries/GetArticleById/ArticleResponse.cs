using Blog.Domain.Articles.Entities;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    string Title,
    string Content,
    Category Category,
    DateTime CratedAt,
    DateTime UpdatedAt
);
