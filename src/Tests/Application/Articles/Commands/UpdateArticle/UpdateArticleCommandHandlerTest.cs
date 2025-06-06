using Blog.Application.Articles.Commands.UpdateArticle;
using Blog.Application.Articles.Services;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Commands.UpdateArticle;

[TestFixture]
class UpdateArticleCommandHandlerTest
{
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly Mock<ITextProcessor> _textProcessor = new();
    private static readonly Category _category = Category.Create("category", "description", false);
    private static readonly Article _article = Article.Create("title", "content", _category);
    private static readonly UpdateArticleCommandHandler _handler =
        new(_articleRepository.Object, _textProcessor.Object);

    [Test]
    public async Task HandlerShouldUpdateArticle()
    {
        var id = 0;
        var newTitle = "new title";
        var newContent = "new content";
        SetupToGetArticleById(id);
        SetUpToGetProcessedText(newContent);
        var command = new UpdateArticleCommand(id, newTitle, newContent);
        await _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _articleRepository.Verify(r => r.GetById(It.Is<int>(i => i == id)), Times.Once);
            _textProcessor.Verify(
                r => r.SanitizeMarkdownToHtml(It.Is<string>(s => s == newContent)),
                Times.Once
            );
            _articleRepository.Verify(
                r => r.Update(It.Is<Article>(p => p.Title == newTitle && p.Content == newContent)),
                Times.Once
            );
        });
    }

    private static void SetupToGetArticleById(int id) =>
        _articleRepository.Setup(s => s.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_article);

    private static void SetUpToGetProcessedText(string content) =>
        _textProcessor
            .Setup(s => s.SanitizeMarkdownToHtml(It.Is<string>(s => s == content)))
            .Returns(content);
}
