using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdQueryHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetArticleByIdQuery, ArticleResponse>
{
    public async Task<ArticleResponse> Handle(
        GetArticleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var article = await articleRepository.GetById(request.Id);
        return new(
            article.Title!,
            article.Content!,
            article.Category,
            article.CreatedAt,
            article.UpdatedAt
        );
    }
}
