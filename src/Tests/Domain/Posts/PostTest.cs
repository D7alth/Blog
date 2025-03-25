using Blog.Domain.Posts;
using Blog.Domain.Shared.Exceptions;
using Bogus;

namespace Blog.Tests.Domain.Posts;

[Category("Post.Domain")]
[TestFixture]
public class PostTest
{
    private const string Title = "Any Title";
    private const string Content = "Any Content";
    private readonly List<string> _tags = ["books", "API", "Clean Code"];
    private readonly Faker _faker = new();

    [SetUp]
    public void Setup() { }

    [Test]
    public void ShouldCreatePost()
    {
        var post = Post.Create(Title, Content);
        Assert.Multiple(() =>
        {
            Assert.That(post.Title, Is.EqualTo(Title));
            Assert.That(post.Content, Is.EqualTo(Content));
        });
    }

    [Test]
    public void ShouldCreatePostWithTags()
    {
        var post = Post.Create(Title, Content, _tags);
        Assert.Multiple(() =>
        {
            Assert.That(post.Tags.First().Name, Is.EqualTo(_tags.First()));
            Assert.That(post.Tags.Last().Name, Is.EqualTo(_tags.Last()));
        });
    }

    [Test]
    public void ShouldNotCreatePostWithDuplicateTags()
    {
        var duplicateTagArr = new List<string>() { _tags[0], _tags[0], _tags[0] };
        var post = Post.Create(Title, Content, duplicateTagArr);
        Assert.That(post.Tags, Has.Count.EqualTo(1));
    }

    [Test]
    public void ShouldUpdatePost()
    {
        var post = Post.Create(Title, Content);
        var firstTimeUpdated = post.UpdatedAt;
        post.Update("new title", "new content");
        var secondTimeUpdated = post.UpdatedAt;
        Assert.Multiple(() =>
        {
            Assert.That(post.Title, Is.EqualTo("new title"));
            Assert.That(post.Content, Is.EqualTo("new content"));
            Assert.That(secondTimeUpdated, Is.Not.EqualTo(firstTimeUpdated));
        });
    }

    [Test]
    public void ShouldReturnExceptionWhenContentAndTitleIsNullToUpdate() =>
        Assert.Throws<NullOrEmptyException>(() =>
        {
            var post = Post.Create(Title, Content);
            post.Update();
        });

    [Test]
    public void ShouldNotCreatePostWithEmptyTitle() =>
        Assert.Throws<NullOrEmptyException>(() => Post.Create("", Content));

    [Test]
    public void ShouldNotCreatePostWithTitleTooBig() =>
        Assert.Throws<ArgumentException>(
            () => Post.Create(_faker.Rant.Random.AlphaNumeric(90), Content)
        );

    [Test]
    public void ShouldNotCreatePostWithEmptyContent() =>
        Assert.Throws<NullOrEmptyException>(() => Post.Create(Title, ""));
}
