using System.Text.RegularExpressions;
using Blog.Domain.Shared;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Posts.ValueObjects;

public partial class Author(string name, string email) : ValueObject
{
    public string Name { get; } = name; 
    public string Email { get; } = email;
    public static Author Create(string name, string email) =>
        string.IsNullOrEmpty(name)
            ? throw new NullOrEmptyException(nameof(name))
            : !EmailRegex().IsMatch(email)
                ? throw new NotValidException(nameof(email))
                : new Author(name, email);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Email;
    }
    
    private const string MatchEmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    [GeneratedRegex(MatchEmailRegex)]
    private static partial Regex EmailRegex();
}