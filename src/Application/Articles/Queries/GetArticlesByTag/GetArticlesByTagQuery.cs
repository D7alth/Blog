using MediatR;

namespace Blog.Application.Articles.Queries.GetArticlesByTag;

public sealed record GetArticlesByTagQuery(string TagName, int Limit, int Page)
    : IRequest<IEnumerable<ArticleResponse>>;
