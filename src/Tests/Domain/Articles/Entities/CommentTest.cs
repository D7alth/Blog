using Blog.Domain.Articles.Entities;
using Blog.Domain.Shared.Exceptions;
using Bogus;

namespace Blog.Tests.Domain.Articles.Entities;

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
        var articleId = _faker.Random.Int();
        var comment = Comment.Create(AuthorId, articleId, Content, true, true);
        Assert.Multiple(() =>
        {
            Assert.That(comment.AuthorId, Is.EqualTo(AuthorId));
            Assert.That(comment.Content, Is.EqualTo(Content));
            Assert.That(comment.ArticleId, Is.EqualTo(articleId));
        });
    }

    [Test]
    public void ShouldReplyComment()
    {
        var articleId = _faker.Random.Int();
        var comment = Comment.Create(AuthorId, articleId, Content, true, true);
        comment.Reply(AuthorId, Content, true);
        Assert.Multiple(() =>
        {
            Assert.That(comment.AuthorId, Is.EqualTo(AuthorId));
            Assert.That(comment.Content, Is.EqualTo(Content));
            Assert.That(comment.ArticleId, Is.EqualTo(articleId));
            Assert.That(comment.Comments.First().ArticleId, Is.EqualTo(articleId));
        });
    }

    [Test]
    public void ShouldNotCreateCommentWithArticleIdInvalid() =>
        Assert.Throws<NotValidException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), Content, false, true)
        );

    [Test]
    public void ShouldNotCreateCommentWithAuthorIdInvalid() =>
        Assert.Throws<NotValidException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), Content, true, false)
        );

    [Test]
    public void ShouldNotCreateArticleWithEmptyContent() =>
        Assert.Throws<NullOrEmptyException>(
            () => Comment.Create(AuthorId, _faker.Random.Int(), "", true, true)
        );
}
