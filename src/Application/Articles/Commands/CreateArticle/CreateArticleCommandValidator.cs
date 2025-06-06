using FluentValidation;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(r => r.Title).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.Content).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.CategoryId)
            .GreaterThan(0)
            .NotNull()
            .WithMessage("Must be use a valid and positive Category ID");
    }
}
