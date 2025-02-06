using Blog.Application.Posts.Commands.Create;
using Blog.Application.Posts.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Posts.Commands.Create;

[TestFixture]
class CreateCommandHandlerTest
{
    private static readonly Mock<IPostRepository> _postRepository = new();
    private static readonly Mock<ITextProcessor> _textProcessor = new();
    private static readonly CreateCommandHandler _handler =
        new(_postRepository.Object, _textProcessor.Object);

    [Test]
    public void HandlerShouldCreateNewPost()
    {
        var title = "title";
        var content = "content";
        SetUpToGetProcessedText(content);
        var command = new CreateCommand(title, content);
        _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _textProcessor.Verify(r => r.Sanitize(It.Is<string>(s => s == content)), Times.Once);
            _postRepository.Verify(
                r => r.Add(It.Is<Post>(p => p.Title == title && p.Content == content)),
                Times.Once
            );
        });
    }

    private static void SetUpToGetProcessedText(string content) =>
        _textProcessor.Setup(s => s.Sanitize(It.Is<string>(s => s == content))).Returns(content);
}
