using MediatR;

namespace Blog.PostContext.Application.Articles.Commands.DeleteArticle;

public sealed record DeleteArticleCommand(int Id) : IRequest;
