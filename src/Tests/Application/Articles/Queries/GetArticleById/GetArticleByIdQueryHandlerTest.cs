using Blog.Application.Articles.Queries.GetArticleById;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetArticleById;

[TestFixture]
class GetArticleByIdQueryHandlerTest
{
    private static readonly Mock<IArticleRepository> _articlesRepository = new();
    private static readonly Article _article = Article.Create("title", "content");
    private static readonly GetArticleByIdQueryHandler _handler = new(_articlesRepository.Object);

    [Test]
    public async Task HandlerShouldReturnAllArticles()
    {
        var articleId = 0;
        SetupToGetArticleById(articleId);
        var query = new GetArticleByIdQuery(articleId);
        var article = await _handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(article.Title, Is.EqualTo(_article.Title));
            Assert.That(article.Content, Is.EqualTo(_article.Content));
            Assert.That(article.Tags.ToList(), Has.Count.EqualTo(_article.Tags.Count));
        });
        _articlesRepository.Verify(r => r.GetById(It.Is<int>(i => i == articleId)), Times.Once);
    }

    private static void SetupToGetArticleById(int id) =>
        _articlesRepository.Setup(s => s.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_article);
}
