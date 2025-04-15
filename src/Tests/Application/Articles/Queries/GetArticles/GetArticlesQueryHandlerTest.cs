using Blog.Application.Articles.Queries.GetArticles;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Bogus;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetArticles;

[TestFixture]
class GetAllArticlesQueryHandlerTest
{
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly List<Article> _articleList =
    [
        Article.Create("title", "content"),
        Article.Create("title 2", "content 2")
    ];
    private static readonly GetArticlesQueryHandler _handler = new(_articleRepository.Object);

    public GetAllArticlesQueryHandlerTest() { }

    [Test]
    public async Task HandlerShouldReturnAllArticles()
    {
        SetUpToGetArticlesAsync();
        var query = new GetArticlesQuery(null, null, 10, 1);
        var articles = await _handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(articles.Count(), Is.EqualTo(_articleList.Count));
            Assert.That(articles.First().Id, Is.EqualTo(_articleList.First().Id));
            Assert.That(articles.Last().Id, Is.EqualTo(_articleList.Last().Id));
            // Assert.That(
            //     articles.First().GetType().GetProperties(),
            //     Has.Length.EqualTo(_articleList.First().GetType().GetProperties().Length)
            // );
        });
        _articleRepository.Verify(r => r.GetArticlesAsync(null, null, 10, 1), Times.Once);
    }

    private static void SetUpToGetArticlesAsync() =>
        _articleRepository
            .Setup(s => s.GetArticlesAsync(null, null, It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(_articleList);
}
