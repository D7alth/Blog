namespace Blog.Domain.Posts.ValueObjects;

public readonly partial struct Tag(string name)
{
    public string Name { get; } = name;

    public static Tag Create(string name) => new(name);
}
