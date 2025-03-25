using MediatR;

namespace Blog.Application.Articles.Commands.UpdateArticle;

public sealed record UpdateArticleCommand(int Id, string Title, string Content) : IRequest;
