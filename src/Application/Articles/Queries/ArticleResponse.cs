namespace Blog.PostContext.Application.Articles.Queries;

public sealed record ArticleResponse(
    int Id,
    string Title,
    string Content,
    int CategoryId,
    DateTime CratedAt,
    DateTime UpdatedAt
);
