using MediatR;

namespace Blog.Application.Articles.Commands.DeleteArticle;

public sealed record DeleteArticleCommand(int Id) : IRequest;
