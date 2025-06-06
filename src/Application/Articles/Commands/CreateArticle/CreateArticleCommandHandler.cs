using Blog.Application.Articles.Services;
using Blog.Domain.Articles;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandHandler(
    IArticleRepository articleRepository,
    ICategoryRepository categoryRepository,
    ITextProcessor textProcessor
) : IRequestHandler<CreateArticleCommand>
{
    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var category =
            await categoryRepository.GetCategoryById(request.CategoryId)
            ?? throw new KeyNotFoundException();
        var article = Article.Create(
            request.Title,
            textProcessor.SanitizeMarkdownToHtml(request.Content),
            category
        );
        articleRepository.Add(article);
    }
}
