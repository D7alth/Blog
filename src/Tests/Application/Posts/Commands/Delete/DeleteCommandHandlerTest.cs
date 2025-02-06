using Blog.Application.Posts.Commands.Delete;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Posts.Commands.Delete;

[TestFixture]
class DeleteCommandHandlerTest
{
    private static readonly Mock<IPostRepository> _postRepository = new();
    private static readonly DeleteCommandHandler _handler = new(_postRepository.Object);
    private static readonly Post _defaultPost = Post.Create("title", "content");

    [Test]
    public async Task HandlerShouldDeletePost()
    {
        var postId = 0;
        SetupToGetPost(postId);
        var command = new DeleteCommand(postId);
        await _handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            _postRepository.Verify(r => r.GetById(It.Is<int>(p => p == postId)), Times.Once);
            _postRepository.Verify(r => r.Remove(It.Is<Post>(p => p.Id == postId)), Times.Once);
        });
    }

    [Test]
    public void HandlerShouldThrowsKeyNotFoundWhenPostDoesExist()
    {
        var command = new DeleteCommand(0);
        _postRepository.Setup(r => r.GetById(It.IsAny<int>()))!.ReturnsAsync(null as Post);
        Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
    }

    private static void SetupToGetPost(int id) =>
        _postRepository.Setup(r => r.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_defaultPost);
}
