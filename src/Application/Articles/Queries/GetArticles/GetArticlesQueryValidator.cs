using FluentValidation;

namespace Blog.Application.Articles.Queries.GetArticles;

public sealed class GetArticlesQueryValidator : AbstractValidator<GetArticlesQuery>
{
    public GetArticlesQueryValidator()
    {
        RuleFor(r => r)
            .Must((r) => r.StartDate < r.EndDate)
            .WithMessage("Start date dot'n be greater than End date")
            .When(r => r.StartDate is not null && r.EndDate is not null);

        RuleFor(r => r)
            .Must((r) => r.StartDate > DateTime.Now)
            .WithMessage("Start date is invalid")
            .When(r => r.StartDate is not null && r.EndDate is null);
    }
}
