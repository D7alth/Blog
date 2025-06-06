using Blog.Application.Articles.Commands.CreateArticle;
using FluentValidation.TestHelper;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Commands.CreateArticle;

[TestFixture]
class CreateArticleCommandValidatorTest
{
    private static readonly CreateArticleCommandValidator _validator = new();
    private static readonly CreateArticleCommand _command = new("Title", "Content", 1);

    [Test]
    public void ShouldNotHaveErrorWithValidCommand()
    {
        var result = _validator.TestValidate(_command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldHaveErrorWhenTitleIsNullOrEmpty()
    {
        var errorMessage = "Must be minimum Length is 1";
        var invalidCommand = new CreateArticleCommand("", "Content", 1);
        var result = _validator.TestValidate(invalidCommand);
        var errorDetails = result.ShouldHaveValidationErrorFor(article => article.Title);
        Assert.That(errorMessage, Is.EqualTo(errorDetails.First().ErrorMessage));
    }

    [Test]
    public void ShouldHaveErrorWhenContentIsNullOrEmpty()
    {
        var invalidCommand = new CreateArticleCommand("Title", "", 1);
        var result = _validator.TestValidate(invalidCommand);
        result.ShouldHaveValidationErrorFor(article => article.Content);
    }
    
    [Test]
    public void ShouldHaveErrorWhenCategoryIdIsLessToOne()
    {
        var invalidCommand = new CreateArticleCommand("Title", "", 0);
        var result = _validator.TestValidate(invalidCommand);
        result.ShouldHaveValidationErrorFor(article => article.CategoryId);
    }
}
