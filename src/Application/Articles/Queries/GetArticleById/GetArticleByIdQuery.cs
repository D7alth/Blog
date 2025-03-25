using MediatR;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed record GetArticleByIdQuery(int Id) : IRequest<ArticleResponse>;
