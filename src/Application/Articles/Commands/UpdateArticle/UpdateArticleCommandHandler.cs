using Blog.Application.Articles.Services;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.UpdateArticle;

public sealed class UpdateArticleCommandHandler(
    IArticleRepository articleRepository,
    ITextProcessor textProcessor
) : IRequestHandler<UpdateArticleCommand>
{
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetById(request.Id);
        article.Update(request.Title, textProcessor.SanitizeMarkdownToHtml(request.Content));
        articleRepository.Update(article);
    }
}
