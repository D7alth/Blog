using MediatR;

namespace Blog.Application.Posts.Commands.Update;

public sealed record UpdateCommand(int Id, string Title, string Content) : IRequest;
