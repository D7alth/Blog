using MediatR;

namespace Blog.Application.Tags.Commands.CreateTag;

public sealed record CreateTagCommand(string TagName) : IRequest;
