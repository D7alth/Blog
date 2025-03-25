using Blog.Application.Posts.Commands.Update;
using Blog.Application.Posts.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Posts.Commands.Update;

[TestFixture]
class UpdateCommandHandlerTest
{
    private static readonly Mock<IPostRepository> _postRepository = new();
    private static readonly Mock<ITextProcessor> _textProcessor = new();
    private static readonly Post _defaultPost = Post.Create("title", "content");
    private static readonly UpdateCommandHandler _handler =
        new(_postRepository.Object, _textProcessor.Object);

    [Test]
    public async Task HandlerShouldUpdateNewPost()
    {
        var id = 0;
        var newTitle = "new title";
        var newContent = "new content";
        SetUpToGetById(id);
        SetUpToGetProcessedText(newContent);
        var command = new UpdateCommand(id, newTitle, newContent);
        await _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _postRepository.Verify(r => r.GetById(It.Is<int>(i => i == id)), Times.Once);
            _textProcessor.Verify(
                r => r.SanitizeMarkdownToHtml(It.Is<string>(s => s == newContent)),
                Times.Once
            );
            _postRepository.Verify(
                r => r.Update(It.Is<Post>(p => p.Title == newTitle && p.Content == newContent)),
                Times.Once
            );
        });
    }

    private static void SetUpToGetById(int id) =>
        _postRepository.Setup(s => s.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_defaultPost);

    private static void SetUpToGetProcessedText(string content) =>
        _textProcessor
            .Setup(s => s.SanitizeMarkdownToHtml(It.Is<string>(s => s == content)))
            .Returns(content);
}
