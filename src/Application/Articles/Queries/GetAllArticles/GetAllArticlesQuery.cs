using MediatR;

namespace Blog.Application.Articles.Queries.GetAllArticles;

public sealed record GetAllArticlesQuery() : IRequest<IEnumerable<ArticleResponse>>;
