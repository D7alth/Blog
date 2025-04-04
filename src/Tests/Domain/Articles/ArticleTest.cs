using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;
using Blog.Domain.Shared.Exceptions;
using Bogus;

namespace Blog.Tests.Domain.Articles;

[TestFixture]
public class ArticleTest
{
    private const string Title = "Any Title";
    private const string Content = "Any Content";
    private readonly List<string> _tags = ["books", "API", "Clean Code"];
    private readonly Faker _faker = new();

    [SetUp]
    public void Setup() { }

    [Test]
    public void ShouldCreateArticle()
    {
        var article = Article.Create(Title, Content);
        Assert.Multiple(() =>
        {
            Assert.That(article.Title, Is.EqualTo(Title));
            Assert.That(article.Content, Is.EqualTo(Content));
        });
    }

    [Test]
    public void ShouldCreateArticleWithTags()
    {
        var tags = _tags.Select(t => Tag.Create(t, false)).ToList();
        var article = Article.Create(Title, Content, tags);
        Assert.Multiple(() =>
        {
            Assert.That(article.Tags.First().Name, Is.EqualTo(_tags.First()));
            Assert.That(article.Tags.Last().Name, Is.EqualTo(_tags.Last()));
        });
    }

    [Test]
    public void ShouldUpdateArticle()
    {
        var article = Article.Create(Title, Content);
        var firstTimeUpdated = article.UpdatedAt;
        article.Update("new title", "new content");
        var secondTimeUpdated = article.UpdatedAt;
        Assert.Multiple(() =>
        {
            Assert.That(article.Title, Is.EqualTo("new title"));
            Assert.That(article.Content, Is.EqualTo("new content"));
            Assert.That(secondTimeUpdated, Is.Not.EqualTo(firstTimeUpdated));
        });
    }

    [Test]
    public void ShouldReturnExceptionWhenContentAndTitleIsNullToUpdate() =>
        Assert.Throws<NullOrEmptyException>(() =>
        {
            var article = Article.Create(Title, Content);
            article.Update();
        });

    [Test]
    public void ShouldNotCreateArticleWithEmptyTitle() =>
        Assert.Throws<NullOrEmptyException>(() => Article.Create("", Content));

    [Test]
    public void ShouldNotCreateArticleWithTitleTooBig() =>
        Assert.Throws<ArgumentException>(
            () => Article.Create(_faker.Rant.Random.AlphaNumeric(90), Content)
        );

    [Test]
    public void ShouldNotCreateArticleWithEmptyContent() =>
        Assert.Throws<NullOrEmptyException>(() => Article.Create(Title, ""));
}
