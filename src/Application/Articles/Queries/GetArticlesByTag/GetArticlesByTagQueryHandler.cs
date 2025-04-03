using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetArticlesByTag;

public sealed class GetArticlesByTagQueryHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetArticlesByTagQuery, IEnumerable<ArticleResponse>>
{
    public async Task<IEnumerable<ArticleResponse>> Handle(
        GetArticlesByTagQuery request,
        CancellationToken cancellationToken
    )
    {
        var articles = await articleRepository.GetArticlesByTagAsync(
            request.TagName,
            request.Limit,
            request.Page
        );
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
