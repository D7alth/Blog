using Blog.Application.Categories.Commands.CreateCategory;
using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using Moq;

namespace Blog.Tests.Application.Categories.Commands.CreateCategory;

[TestFixture]
public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new();
    private CreateCategoryCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new CreateCategoryCommandHandler(_categoryRepositoryMock.Object);
    }

    [Test]
    public async Task HandleWShouldCreateAndAddCategory()
    {
        SetupToExistsAsync(false);
        var command = new CreateCategoryCommand("TestCategory", "description");
        await _handler.Handle(command, CancellationToken.None);
        _categoryRepositoryMock.Verify(x => x.ExistsAsync(command.Name), Times.Once);
        _categoryRepositoryMock.Verify(
            x => x.Add(It.Is<Category>(t => t.Name == command.Name)),
            Times.Once
        );
    }

    [Test]
    public void HandleWhenCategoryExistsShouldThrowArgumentException()
    {
        SetupToExistsAsync(true);
        var command = new CreateCategoryCommand("ExistingCategory", "description");
        var exception = Assert.ThrowsAsync<ArgumentException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
        Assert.That(exception, Is.Not.Null);
        _categoryRepositoryMock.Verify(x => x.ExistsAsync(command.Name), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    }

    private void SetupToExistsAsync(bool exists) =>
        _categoryRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(exists);
}
