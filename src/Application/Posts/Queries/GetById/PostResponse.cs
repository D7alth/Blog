namespace Blog.Application.Posts.Queries.GetById;

public sealed record PostResponse(
    string Title,
    string Content,
    DateTime CratedAt,
    DateTime UpdatedAt
);
