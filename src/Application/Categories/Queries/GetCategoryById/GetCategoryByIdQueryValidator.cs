using FluentValidation;

namespace Blog.Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(r => r.CategoryId).GreaterThan(0).NotNull().WithMessage("Must be value should greeter than 1");
    }
}
