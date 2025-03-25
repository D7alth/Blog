using Blog.Application.Articles.Queries.GetAllArticles;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetAllArticles;

[TestFixture]
class GetAllArticlesQueryHandlerTest
{
    private static readonly Mock<IPostRepository> _postRepository = new();
    private static readonly List<Post> _postList =
    [
        Post.Create("title", "content"),
        Post.Create("title 2", "content 2")
    ];
    private static readonly GetAllArticlesQueryHandler _handler = new(_postRepository.Object);
    private static readonly GetAllArticlesQuery _query = new();

    [Test]
    public async Task HandlerShouldReturnAllPosts()
    {
        SetUpToGetAll();
        var posts = await _handler.Handle(_query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(posts.Count(), Is.EqualTo(_postList.Count));
            Assert.That(posts.First().Id, Is.EqualTo(_postList.First().Id));
            Assert.That(posts.Last().Id, Is.EqualTo(_postList.Last().Id));
            Assert.That(
                posts.First().GetType().GetProperties(),
                Has.Length.EqualTo(_postList.First().GetType().GetProperties().Length)
            );
        });
        _postRepository.Verify(r => r.GetAll(), Times.Once);
    }

    private static void SetUpToGetAll() =>
        _postRepository.Setup(s => s.GetAll()).ReturnsAsync(_postList);
}
