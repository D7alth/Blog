using Blog.Domain.Posts.ValueObjects;

namespace Blog.Application.Posts.Queries.GetById;

public sealed record PostResponse(
    string Title,
    string Content,
    List<Tag> Tags,
    DateTime CratedAt,
    DateTime UpdatedAt
);
