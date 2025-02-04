using MediatR;

namespace Blog.Application.Posts.Commands.Create;

public sealed record CreateCommand(string Title, string Content) : IRequest;
