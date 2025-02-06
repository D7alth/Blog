using MediatR;

namespace Blog.Application.Posts.Commands.Delete;

public sealed record DeleteCommand(int Id) : IRequest;
