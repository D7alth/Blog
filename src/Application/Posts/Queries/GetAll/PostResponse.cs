namespace Blog.Application.Features.Posts.Queries.GetPosts;

public sealed record PostResponse(
    int Id,
    string Title,
    string Content,
    DateTime CratedAt,
    DateTime UpdatedAt
);
