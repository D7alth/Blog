using Blog.Application.Articles.Commands.DeleteArticle;
using Blog.Domain.Articles.Repositories;
using FluentValidation;

namespace Blog.Application.Articles.Commands.UpdateArticle;

public sealed class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator(IArticleRepository articleRepository)
    {
        RuleFor(r => r.Id).GreaterThan(0).WithMessage("Must be use a valid and positive ID");
        RuleFor(r => r.Title).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.Content).MinimumLength(1).WithMessage("Must be minimum Length is 1");
        RuleFor(r => r.Id)
            .MustAsync(async (id, _) => await articleRepository.ExistsAsync(id))
            .WithMessage("Article not found");
    }
}
