using Blog.Application.Articles.Commands.UpdateArticle;
using Blog.Domain.Articles.Repositories;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Commands.UpdateArticle;

[TestFixture]
class UpdateArticleCommandValidatorTest
{
    private UpdateArticleCommandValidator _validator;
    private UpdateArticleCommand _command;
    private static readonly Mock<IArticleRepository> _articleRepository = new();

    [SetUp]
    public void Setup()
    {
        _validator = new(_articleRepository.Object);
        _command = new(1, "title", "content");
    }

    [Test]
    public async Task ShouldNotHaveErrorWithValidCommand()
    {
        SetupToGetExistsAsync(true);
        var result = await _validator.TestValidateAsync(_command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ShouldHaveErrorWhenTitleIsNullOrEmpty()
    {
        var errorMessage = "Must be minimum Length is 1";
        var invalidCommand = new UpdateArticleCommand(1, "", "Content");
        var result = await _validator.TestValidateAsync(invalidCommand);
        var errorDetails = result.ShouldHaveValidationErrorFor(article => article.Title);
        Assert.That(errorMessage, Is.EqualTo(errorDetails.First().ErrorMessage));
    }

    [Test]
    public async Task ShouldHaveErrorWhenContentIsNullOrEmpty()
    {
        var invalidCommand = new UpdateArticleCommand(1, "Title", "");
        var result = await _validator.TestValidateAsync(invalidCommand);
        result.ShouldHaveValidationErrorFor(article => article.Content);
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public async Task ShouldHaveErrorWhenIdIsLessToOne(int id)
    {
        SetupToGetExistsAsync(true);
        var errorMessage = @"'Id' must be greater than '0'.";
        var invalidCommand = new UpdateArticleCommand(id, "Title", "Content");
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
