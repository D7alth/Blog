using Blog.Application.Articles.Queries.GetArticleById;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetArticleById;

[TestFixture]
class GetArticleByIdQueryHandlerTest
{
    private static readonly Mock<IPostRepository> _postRepository = new();
    private static readonly Post _post = Post.Create("title", "content");
    private static readonly GetArticleByIdQueryHandler _handler = new(_postRepository.Object);

    [Test]
    public async Task HandlerShouldReturnAllPosts()
    {
        var postId = 0;
        SetUpGetById(postId);
        var query = new GetArticleByIdQuery(postId);
        var post = await _handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(post.Title, Is.EqualTo(_post.Title));
            Assert.That(post.Content, Is.EqualTo(_post.Content));
            Assert.That(post.Tags, Has.Count.EqualTo(_post.Tags.Count));
        });
        _postRepository.Verify(r => r.GetById(It.Is<int>(i => i == postId)), Times.Once);
    }

    private static void SetUpGetById(int id) =>
        _postRepository.Setup(s => s.GetById(It.Is<int>(i => i == id))).ReturnsAsync(_post);
}
