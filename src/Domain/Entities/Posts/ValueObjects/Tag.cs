namespace Blog.Domain.Entities.Posts.ValueObjects;

public sealed class Tag(string Name) : ValueObject
{
    public string Name { get; } = Name;

    public static Tag Create(string name) =>
        string.IsNullOrEmpty(name)
            ? throw new ArgumentException("Tag name cannot be null")
            : new Tag(name);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
