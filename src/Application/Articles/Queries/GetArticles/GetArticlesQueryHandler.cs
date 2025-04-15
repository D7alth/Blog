using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetArticles;

public sealed class GetArticlesQueryHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetArticlesQuery, IEnumerable<ArticleResponse>>
{
    public async Task<IEnumerable<ArticleResponse>> Handle(
        GetArticlesQuery request,
        CancellationToken cancellationToken
    )
    {
        var articles = await articleRepository.GetArticlesAsync(
            request.StartDate,
            request.EndDate,
            request.Limit,
            request.Page
        );
        return articles.Select(article => new ArticleResponse(
            article.Id,
            article.Title!,
            article.Content!,
            article.Category,
            article.CreatedAt,
            article.UpdatedAt
        ));
    }
}
