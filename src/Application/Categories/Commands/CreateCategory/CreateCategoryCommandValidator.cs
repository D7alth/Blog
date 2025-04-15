using FluentValidation;

namespace Blog.Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(r => r.Name).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.Name).MaximumLength(50).WithMessage("Must be maximum Length is 50");
        RuleFor(r => r.Name).NotEmpty().WithMessage("Must be not empty");
    }
}
