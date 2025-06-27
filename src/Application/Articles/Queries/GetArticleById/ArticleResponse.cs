using Blog.PostContext.Domain.Articles.Entities;

namespace Blog.PostContext.Application.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    string Title,
    string Content,
    Category Category,
    DateTime CratedAt,
    DateTime UpdatedAt
);
