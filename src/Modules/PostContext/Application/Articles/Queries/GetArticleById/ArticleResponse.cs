namespace Blog.PostContext.Application.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    string Title,
    string Content,
    int CategoryId,
    DateTime CratedAt,
    DateTime UpdatedAt
);
