using Blog.Domain.Entities.Posts;
using Bogus;

namespace Blog.Tests.Domain.Entities.Posts;

[Category("Post.Domain")]
public class PostTests
{
    private readonly string _title = "Any Title";
    private readonly string _content = "Any Content";
    private readonly List<string> tags = ["books", "API", "Clean Code"];
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
    public void ShouldAddTagToPost()
    {
        var post = Post.Create(_title, _content);
        tags.ForEach(post.AddTag);
        Assert.Multiple(() =>
        {
            Assert.That(post.Tags.First().Name, Is.EqualTo(tags.First()));
            Assert.That(post.Tags.Last().Name, Is.EqualTo(tags.Last()));
        });
    }

    [Test]
    public void ShouldNotAddDuplicateTagsToSamePost()
    {
        var post = Post.Create(_title, _content);
        post.AddTag(tags[0]);
        post.AddTag(tags[0]);
        post.AddTag(tags[0]);
        Assert.Multiple(() =>
        {
            Assert.That(post.Tags, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void ShouldNotCreatePostWithEmptyTitle() =>
        Assert.Throws<ArgumentException>(() => Post.Create("", _content));

    [Test]
    public void ShouldNotCreatePostWithTitleTooBig() =>
        Assert.Throws<ArgumentException>(
            () => Post.Create(_faker.Rant.Random.AlphaNumeric(90), _content)
        );

    [Test]
    public void ShouldNotCreatePostWithEmptyContent() =>
        Assert.Throws<ArgumentException>(() => Post.Create(_title, ""));
}
