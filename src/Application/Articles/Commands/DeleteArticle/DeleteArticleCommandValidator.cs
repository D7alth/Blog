using Blog.Domain.Articles.Repositories;
using FluentValidation;

namespace Blog.Application.Articles.Commands.DeleteArticle;

public sealed class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator(IArticleRepository articleRepository)
    {
        RuleFor(r => r.Id).GreaterThan(0).WithMessage("Must be use a valid and positive ID");
        RuleFor(r => r.Id)
            .MustAsync(async (id, _) => await articleRepository.ExistsAsync(id))
            .WithMessage("Article not found");
    }
}
