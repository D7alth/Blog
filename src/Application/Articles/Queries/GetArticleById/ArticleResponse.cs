using Blog.Domain.Posts.ValueObjects;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    string Title,
    string Content,
    List<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
