using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts.ValueObjects;

public sealed class Tag(string name) : ValueObject
{
    public string Name { get; } = name;

    public static Tag Create(string name) =>
        string.IsNullOrEmpty(name)
            ? throw new NullOrEmptyException(nameof(name))
            : new Tag(name);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
