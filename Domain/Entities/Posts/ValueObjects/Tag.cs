namespace Blog.Domain.Entities.Posts.ValueObjects;

public sealed class Tag(string name)
{
    public string Name { get; } = name;
    
    public static Tag Create(string name) => string.IsNullOrEmpty(name)
        ? throw new ArgumentException("Tag name cannot be null")
        : new Tag(name);
}