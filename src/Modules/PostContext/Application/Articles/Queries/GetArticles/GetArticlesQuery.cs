using MediatR;

namespace Blog.PostContext.Application.Articles.Queries.GetArticles;

public sealed record GetArticlesQuery(DateTime? StartDate, DateTime? EndDate, int Limit, int Page)
    : IRequest<IEnumerable<ArticleResponse>>;
