using Blog.PostContext.Domain.Articles;
using Blog.PostContext.Domain.Articles.Repositories;
using Blog.PostContext.Application.Articles.Services;
using MediatR;

namespace Blog.PostContext.Application.Articles.Commands.CreateArticle;

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
