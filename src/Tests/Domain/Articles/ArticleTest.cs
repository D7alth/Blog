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
    private readonly Category _category = Category.Create("Clean Code", "Any Description", false);
    private readonly Faker _faker = new();

    [SetUp]
    public void Setup() { }

    [Test]
    public void ShouldCreateArticle()
    {
        var article = Article.Create(Title, Content, _category);
        Assert.Multiple(() =>
        {
            Assert.That(article.Title, Is.EqualTo(Title));
            Assert.That(article.Content, Is.EqualTo(Content));
        });
    }

    [Test]
    public void ShouldCreateArticleWithCategory()
    {
        var article = Article.Create(Title, Content, _category);
        Assert.Multiple(() =>
        {
            Assert.That(article.Category.Name, Is.EqualTo(_category.Name));
        });
    }

    [Test]
    public void ShouldUpdateArticle()
    {
        var article = Article.Create(Title, Content, _category);
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
            var article = Article.Create(Title, Content, _category);
            article.Update();
        });

    [Test]
    public void ShouldNotCreateArticleWithEmptyTitle() =>
        Assert.Throws<NullOrEmptyException>(() => Article.Create("", Content, _category));

    [Test]
    public void ShouldNotCreateArticleWithTitleTooBig() =>
        Assert.Throws<ArgumentException>(
            () => Article.Create(_faker.Rant.Random.AlphaNumeric(90), Content, _category)
        );

    [Test]
    public void ShouldNotCreateArticleWithEmptyContent() =>
        Assert.Throws<NullOrEmptyException>(() => Article.Create(Title, "", _category));
}
