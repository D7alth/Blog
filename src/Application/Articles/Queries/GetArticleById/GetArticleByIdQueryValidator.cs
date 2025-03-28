using Blog.Domain.Articles.Repositories;
using FluentValidation;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdQueryValidator : AbstractValidator<GetArticleByIdQuery>
{
    public GetArticleByIdQueryValidator()
    {
        RuleFor(r => r.Id).GreaterThan(0).WithMessage("Must be use a valid and positive ID");
    }
}
