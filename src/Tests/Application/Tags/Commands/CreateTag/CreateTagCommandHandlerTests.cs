using Blog.Application.Tags.Commands.CreateTag;
using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Moq;

namespace Blog.Tests.Application.Tags.Commands.CreateTag;

[TestFixture]
public class CreateTagCommandHandlerTests
{
    private readonly Mock<ITagRepository> _tagRepositoryMock = new();
    private CreateTagCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new CreateTagCommandHandler(_tagRepositoryMock.Object);
    }

    [Test]
    public async Task HandleWShouldCreateAndAddTag()
    {
        SetupToExistsAsync(false);
        var command = new CreateTagCommand("TestTag");
        await _handler.Handle(command, CancellationToken.None);
        _tagRepositoryMock.Verify(x => x.ExistsAsync(command.TagName), Times.Once);
        _tagRepositoryMock.Verify(
            x => x.Add(It.Is<Tag>(t => t.Name == command.TagName)),
            Times.Once
        );
    }

    [Test]
    public void HandleWhenTagExistsShouldThrowArgumentException()
    {
        SetupToExistsAsync(true);
        var command = new CreateTagCommand("ExistingTag");
        var exception = Assert.ThrowsAsync<ArgumentException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
        Assert.That(exception, Is.Not.Null);
        _tagRepositoryMock.Verify(x => x.ExistsAsync(command.TagName), Times.Once);
        _tagRepositoryMock.Verify(x => x.Add(It.IsAny<Tag>()), Times.Never);
    }

    private void SetupToExistsAsync(bool exists) =>
        _tagRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(exists);
}
