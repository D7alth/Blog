using FluentValidation;

namespace Blog.Application.Tags.Commands.CreateTag;

public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(r => r.TagName).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.TagName).MaximumLength(50).WithMessage("Must be maximum Length is 50");
        RuleFor(r => r.TagName).NotEmpty().WithMessage("Must be not empty");
    }
}
