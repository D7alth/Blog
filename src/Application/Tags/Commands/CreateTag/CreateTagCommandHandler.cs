using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Tags.Commands.CreateTag;

public sealed class CreateTagCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand>
{
    public async Task Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var isDuplicate = await tagRepository.ExistsAsync(request.TagName);
        var tag = Tag.Create(request.TagName, isDuplicate);
        tagRepository.Add(tag);
    }
}
