using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetAllArticles;

public sealed class GetAllArticlesQueryHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleResponse>>
{
    public async Task<IEnumerable<ArticleResponse>> Handle(
        GetAllArticlesQuery request,
        CancellationToken cancellationToken
    )
    {
        var articles = await articleRepository.GetAll();
        return articles.Select(article => new ArticleResponse(
            article.Id,
            article.Title!,
            article.Content!,
            article.Tags,
            article.CreatedAt,
            article.UpdatedAt
        ));
    }
}
