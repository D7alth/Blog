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
    public void ShouldAddTagToPost()
    {
        var post = Post.Create(Title, Content);
        _tags.ForEach(post.AddTag);
        Assert.Multiple(() =>
        {
            Assert.That(post.Tags.First().Name, Is.EqualTo(_tags.First()));
            Assert.That(post.Tags.Last().Name, Is.EqualTo(_tags.Last()));
        });
    }

    [Test]
    public void ShouldNotAddDuplicateTagsToSamePost()
    {
        var post = Post.Create(Title, Content);
        post.AddTag(_tags[0]);
        post.AddTag(_tags[0]);
        post.AddTag(_tags[0]);
        Assert.Multiple(() =>
        {
            Assert.That(post.Tags, Has.Count.EqualTo(1));
        });
    }

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
