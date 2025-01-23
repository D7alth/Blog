using Blog.Domain.Posts.Entities;
using Blog.Domain.Shared.Exceptions;
using Bogus;

namespace Blog.Tests.Domain.Posts.Entities;

[TestFixture]
public class CommentTest
{
    private readonly Guid AuthorId = Guid.Empty;
    private const string Content = "Any Content";
    private readonly Faker _faker = new();

    [SetUp]
    public void Setup() { }

    [Test]
    public void ShouldCreateComment()
    {
        var postId = _faker.Random.Int();
        var comment = Comment.Create(AuthorId, postId, Content, true, true);
        Assert.Multiple(() =>
        {
            Assert.That(comment.AuthorId, Is.EqualTo(AuthorId));
            Assert.That(comment.Content, Is.EqualTo(Content));
            Assert.That(comment.PostId, Is.EqualTo(postId));
        });
    }

    [Test]
    public void ShouldReplyComment()
    {
        var postId = _faker.Random.Int();
        var comment = Comment.Create(AuthorId, postId, Content, true, true);
        comment.Reply(AuthorId, Content, true);
        Assert.Multiple(() =>
        {
            Assert.That(comment.AuthorId, Is.EqualTo(AuthorId));
            Assert.That(comment.Content, Is.EqualTo(Content));
            Assert.That(comment.PostId, Is.EqualTo(postId));
            Assert.That(comment.Comments.First().PostId, Is.EqualTo(postId));
        });
    }

    [Test]
    public void ShouldNotCreateCommentWithPostIdInvalid() =>
        Assert.Throws<NotValidException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), Content, false, true)
        );

    [Test]
    public void ShouldNotCreateCommentWithAuthorIdInvalid() =>
        Assert.Throws<NotValidException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), Content, true, false)
        );

    [Test]
    public void ShouldNotCreatePostWithEmptyContent() =>
        Assert.Throws<NullOrEmptyException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), "", true, true)
        );
}
