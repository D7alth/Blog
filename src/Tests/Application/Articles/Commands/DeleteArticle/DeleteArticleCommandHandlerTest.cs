using Blog.Application.Articles.Commands.DeleteArticle;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Commands.DeleteArticle;

[TestFixture]
class DeleteArticleCommandHandlerTest
{
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly DeleteArticleCommandHandler _handler = new(_articleRepository.Object);
    private static readonly Article _article = Article.Create("title", "content");

    [Test]
    public async Task HandlerShouldDeleteArticle()
    {
        var articleId = 0;
        SetupToGetArticleById(articleId);
        var command = new DeleteArticleCommand(articleId);
        await _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _articleRepository.Verify(r => r.GetById(It.Is<int>(p => p == articleId)), Times.Once);
            _articleRepository.Verify(
                r => r.Remove(It.Is<Article>(p => p.Id == articleId)),
                Times.Once
            );
        });
    }

    private static void SetupToGetArticleById(int id) =>
        _articleRepository.Setup(r => r.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_article);
}
