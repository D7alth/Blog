using FluentValidation;

namespace Blog.Application.Articles.Queries.GetArticlesByTag;

public sealed class GetArticlesByTagQueryValidator : AbstractValidator<GetArticlesByTagQuery>
{
    public GetArticlesByTagQueryValidator()
    {
        RuleFor(r => r.TagName)
            .NotEmpty()
            .WithMessage("The tag is required")
            .MaximumLength(50)
            .WithMessage("Tag must not have more than 50 characters");
        RuleFor(r => r.Limit).GreaterThan(0).WithMessage("Limit must be greater than 0");
        RuleFor(r => r.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
    }
}
