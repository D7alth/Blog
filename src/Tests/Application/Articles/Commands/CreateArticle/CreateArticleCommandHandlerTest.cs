using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Services;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Moq;

namespace Blog.Tests.Application.Articles.Commands.CreateArticle;

[TestFixture]
class CreateCommandHandlerTest
{
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly Mock<ITextProcessor> _textProcessor = new();
    private static readonly Mock<ICategoryRepository> _categoryRepository = new();
    private static readonly Category _category = Category.Create("category", "description", false);
    private static readonly CreateArticleCommandHandler _handler =
        new(_articleRepository.Object, _categoryRepository.Object, _textProcessor.Object);

    [Test]
    public void HandlerShouldCreateNewArticle()
    {
        var title = "title";
        var content = "content";
        var categoryId = 1;
        SetUpToGetProcessedText(content);
        SetUpToGetCategoryById(categoryId);
        var command = new CreateArticleCommand(title, content, categoryId);
        _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _textProcessor.Verify(
                r => r.SanitizeMarkdownToHtml(It.Is<string>(s => s == content)),
                Times.Once
            );
            _articleRepository.Verify(
                r => r.Add(It.Is<Article>(p => p.Title == title && p.Content == content)),
                Times.Once
            );
        });
    }

    private static void SetUpToGetProcessedText(string content) =>
        _textProcessor
            .Setup(s => s.SanitizeMarkdownToHtml(It.Is<string>(s => s == content)))
            .Returns(content);

    private static void SetUpToGetCategoryById(int id) =>
        _categoryRepository
            .Setup(s => s.GetCategoryById(It.Is<int>(i => i == id)))
            .ReturnsAsync(_category);
}
