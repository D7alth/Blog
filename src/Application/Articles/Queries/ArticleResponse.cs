namespace Blog.Application.Articles.Queries;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    int CategoryId,
    DateTime CratedAt,
    DateTime UpdatedAt
);
