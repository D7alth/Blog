using Blog.Domain.Posts.ValueObjects;

namespace Blog.Application.Articles.Queries.GetAllArticles;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    List<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
