using Blog.Application.Articles.Queries.GetArticlesByTag;
using FluentValidation.TestHelper;

namespace Blog.Tests.Application.Articles.Queries.GetArticlesByTag;

[TestFixture]
class GetArticlesByTagQueryValidatorTest
{
    private static readonly GetArticlesByTagQueryValidator _validator = new();

    [Test]
    public void ShouldNotHaveErrorWithValidQuery()
    {
        var validQuery = new GetArticlesByTagQuery("tag", 10, 1);
        var result = _validator.TestValidate(validQuery);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void ShouldHaveErrorWhenTagNameIsEmpty(string tagName)
    {
        var invalidQuery = new GetArticlesByTagQuery(tagName, 10, 1);
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query.TagName)
            .WithErrorMessage("The tag is required");
    }

    [Test]
    public void ShouldHaveErrorWhenTagNameIsTooLong()
    {
        var longTag = new string('a', 51);
        var invalidQuery = new GetArticlesByTagQuery(longTag, 10, 1);
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query.TagName)
            .WithErrorMessage("Tag must not have more than 50 characters");
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    public void ShouldHaveErrorWhenLimitIsLessThanOne(int limit)
    {
        var invalidQuery = new GetArticlesByTagQuery("tag", limit, 1);
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query.Limit)
            .WithErrorMessage("Limit must be greater than 0");
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    public void ShouldHaveErrorWhenPageIsLessThanOne(int page)
    {
        var invalidQuery = new GetArticlesByTagQuery("tag", 10, page);
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query.Page)
            .WithErrorMessage("Page must be greater than 0");
    }
}
