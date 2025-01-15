namespace Blog.Domain.Entities.Posts.ValueObjects;

public sealed class Tag(string id)
{
    public string Id { get; } = id;

    public static Tag Create(string id) =>
        string.IsNullOrEmpty(id)
            ? throw new ArgumentException("Tag name cannot be null")
            : new Tag(id);
}
