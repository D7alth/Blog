using Blog.Domain.Posts.ValueObjects;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Tests.Domain.ValueObjects;

[TestFixture]
public class AuthorTest
{
    private const string Name = "Alberth";
    private const string Email = "example@mail.com";
    
    [Test]
    public void ShouldCreateTag()
    {
        var author = Author.Create(Name, Email);
        Assert.Multiple(() =>
        {
            Assert.That(author.Name, Is.EqualTo(Name));
            Assert.That(author.Email, Is.EqualTo(Email));
        });
    }

    [Test]
    public void ShouldThrowErrorWhenNameIsNullOrEmpty()
        =>
            Assert.Throws<NullOrEmptyException>(() => Author.Create("", Email));
    
    [Test]
    public void ShouldThrowErrorWhenEmailIsNotValid()
        =>
            Assert.Throws<NotValidException>(() => Author.Create(Name, "invalidEmail.123,"));

    
}