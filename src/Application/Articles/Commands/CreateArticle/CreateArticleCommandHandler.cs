using Blog.Application.Articles.Services;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandHandler(
    IArticleRepository articleRepository,
    ITextProcessor textProcessor
) : IRequestHandler<CreateArticleCommand>
{
    public Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = Article.Create(
            request.Title,
            textProcessor.SanitizeMarkdownToHtml(request.Content),
            request.Tags
        );
        articleRepository.Add(article);
        return Task.CompletedTask;
    }
}
