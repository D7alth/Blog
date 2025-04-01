using Blog.Application.Articles.Queries.GetArticleById;
using FluentValidation.TestHelper;
using NUnit.Framework.Internal;

namespace Blog.Tests.Application.Articles.Queries.GetArticleById;

[TestFixture]
class GetArticleByIdQueryValidatorTest
{
    private static readonly GetArticleByIdQueryValidator _validator = new();
    private static readonly GetArticleByIdQuery _query = new(1);

    [Test]
    public void ShouldNotHaveErrorWithValidCommand()
    {
        var result = _validator.TestValidate(_query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public async Task ShouldHaveErrorWhenIdIsLessToOne(int id)
    {
        var errorMessage = @"'Id' must be greater than '0'.";
        var invalidCommand = new GetArticleByIdQuery(id);
        var result = await _validator.TestValidateAsync(invalidCommand);
        var errorDetails = result.ShouldHaveValidationErrorFor(article => article.Id);
        Assert.That(errorMessage, Is.EqualTo(errorDetails.First().ErrorMessage));
    }
}
