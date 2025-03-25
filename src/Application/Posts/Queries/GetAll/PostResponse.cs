using Blog.Domain.Posts.ValueObjects;

namespace Blog.Application.Posts.Queries.GetAll;

public sealed record PostResponse(
    int Id,
    string Title,
    string Content,
    List<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
