using Blog.PostContext.Domain.Articles.Repositories;
using FluentValidation;

namespace Blog.PostContext.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdQueryValidator : AbstractValidator<GetArticleByIdQuery>
{
    public GetArticleByIdQueryValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThan(0)
            .NotNull()
            .WithMessage("Must be use a valid and positive ID");
    }
}
