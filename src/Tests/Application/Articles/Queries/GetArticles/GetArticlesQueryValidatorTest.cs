using Blog.Application.Articles.Queries.GetArticles;
using FluentValidation.TestHelper;

namespace Blog.Tests.Application.Articles.Queries.GetArticles;

[TestFixture]
class GetArticlesQueryValidatorTest
{
    private static readonly GetArticlesQueryValidator _validator = new();

    [Test]
    public void ShouldNotHaveErrorWithValidDates()
    {
        var validQuery = new GetArticlesQuery(
            DateTime.Now.AddDays(1),
            DateTime.Now.AddDays(2),
            1,
            10
        );
        var result = _validator.TestValidate(validQuery);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldHaveErrorWhenStartDateIsGreaterThanEndDate()
    {
        var invalidQuery = new GetArticlesQuery(
            DateTime.Now.AddDays(2),
            DateTime.Now.AddDays(1),
            1,
            10
        );
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query)
            .WithErrorMessage("Start date dot'n be greater than End date");
    }

    [Test]
    public void ShouldHaveErrorWhenStartDateIsInThePast()
    {
        var invalidQuery = new GetArticlesQuery(DateTime.Now.AddDays(-1), null, 1, 10);
        var result = _validator.TestValidate(invalidQuery);
        result
            .ShouldHaveValidationErrorFor(query => query)
            .WithErrorMessage("Start date is invalid");
    }

    [Test]
    public void ShouldNotHaveErrorWhenStartDateIsNull()
    {
        var validQuery = new GetArticlesQuery(null, DateTime.Now.AddDays(1), 1, 10);
        var result = _validator.TestValidate(validQuery);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldNotHaveErrorWhenEndDateIsNull()
    {
        var validQuery = new GetArticlesQuery(DateTime.Now.AddDays(1), null, 1, 10);
        var result = _validator.TestValidate(validQuery);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
