using MediatR;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed record CreateArticleCommand(string Title, string Content, int CategoryId) : IRequest;
