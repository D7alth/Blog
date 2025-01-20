using Blog.Domain.Entities.Posts;
using Bogus;

namespace Blog.Tests.Domain.Entities.Posts;

[Category("Post.Domain")]
public class PostTests
{
    private readonly string _title = "Any Title";
    private readonly string _content = "Any Content";
    readonly Faker _faker = new();

    [SetUp]
    public void Setup() { }

    [Test]
    public void ShouldCreatePost()
    {
        var post = Post.Create(_title, _content);
        Assert.Multiple(() =>
        {
            Assert.That(post.Title, Is.EqualTo(_title));
            Assert.That(post.Content, Is.EqualTo(_content));
        });
    }

    [Test]
    public void ShouldNotCreatePostWithEmptyTitle()
    {
        Assert.Throws<ArgumentException>(() => Post.Create("", _content));
    }

    [Test]
    public void ShouldNotCreatePostWithTitleTooBig()
    {
        Assert.Throws<ArgumentException>(
            () => Post.Create(_faker.Rant.Random.AlphaNumeric(90), _content)
        );
    }

    [Test]
    public void ShouldNotCreatePostWithEmptyContent()
    {
        Assert.Throws<ArgumentException>(() => Post.Create(_title, ""));
    }
}
