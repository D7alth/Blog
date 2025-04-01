using Blog.Application.Articles.Commands.DeleteArticle;
using Blog.Domain.Articles.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Commands.DeleteArticle;

[TestFixture]
class DeleteArticleCommandValidatorTest
{
    private DeleteArticleCommandValidator _validator;
    private static readonly Mock<IArticleRepository> _articleRepository = new();
    private static readonly DeleteArticleCommand _command = new(1);

    [SetUp]
    public void Setup()
    {
        _validator = new(_articleRepository.Object);
    }

    [Test]
    public async Task ShouldNotHaveErrorWithValidCommand()
    {
        SetupToGetExistsAsync(true);
        var result = await _validator.TestValidateAsync(_command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public async Task ShouldHaveErrorWhenIdIsLessToOne(int id)
    {
        SetupToGetExistsAsync(true);
        var errorMessage = @"'Id' must be greater than '0'.";
        var invalidCommand = new DeleteArticleCommand(id);
        var result = await _validator.TestValidateAsync(invalidCommand);
        var errorDetails = result.ShouldHaveValidationErrorFor(article => article.Id);
        Assert.That(errorMessage, Is.EqualTo(errorDetails.First().ErrorMessage));
    }

    [Test]
    public async Task ShouldHaveErrorWhenArticleDoesExist()
    {
        SetupToGetExistsAsync(false);
        var errorMessage = "Article not found";
        var result = await _validator.TestValidateAsync(_command);
        var errorDetails = result.ShouldHaveValidationErrorFor(article => article.Id);
        Assert.That(errorMessage, Is.EqualTo(errorDetails.First().ErrorMessage));
    }

    private static void SetupToGetExistsAsync(bool isExistent) =>
        _articleRepository.Setup(s => s.ExistsAsync(It.IsAny<int>())).ReturnsAsync(isExistent);
}
