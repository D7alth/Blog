using Blog.Application.Articles.Queries.GetAllArticles;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetAllArticles;

[TestFixture]
class GetAllArticlesQueryHandlerTest
{
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly List<Article> _articleList =
    [
        Article.Create("title", "content"),
        Article.Create("title 2", "content 2")
    ];
    private static readonly GetAllArticlesQueryHandler _handler = new(_articleRepository.Object);
    private static readonly GetAllArticlesQuery _query = new();

    [Test]
    public async Task HandlerShouldReturnAllArticles()
    {
        SetUpToGetAllArticles();
        var articles = await _handler.Handle(_query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(articles.Count(), Is.EqualTo(_articleList.Count));
            Assert.That(articles.First().Id, Is.EqualTo(_articleList.First().Id));
            Assert.That(articles.Last().Id, Is.EqualTo(_articleList.Last().Id));
            Assert.That(
                articles.First().GetType().GetProperties(),
                Has.Length.EqualTo(_articleList.First().GetType().GetProperties().Length)
            );
        });
        _articleRepository.Verify(r => r.GetAll(), Times.Once);
    }

    private static void SetUpToGetAllArticles() =>
        _articleRepository.Setup(s => s.GetAll()).ReturnsAsync(_articleList);
}
