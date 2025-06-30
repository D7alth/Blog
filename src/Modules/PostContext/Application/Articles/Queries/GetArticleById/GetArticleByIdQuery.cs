using MediatR;

namespace Blog.PostContext.Application.Articles.Queries.GetArticleById;

public sealed record GetArticleByIdQuery(int Id) : IRequest<ArticleResponse>;
