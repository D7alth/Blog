namespace Blog.Application.Features.Posts.Queries.GetPosts;

public sealed record PostsResponse(
    int Id,
    string Title,
    string Content,
    DateTime CratedAt,
    DateTime UpdatedAt
);
