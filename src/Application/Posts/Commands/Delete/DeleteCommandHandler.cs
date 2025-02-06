using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Commands.Delete;

public sealed class DeleteCommandHandler(IPostRepository postRepository)
    : IRequestHandler<DeleteCommand>
{
    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var post =
            await postRepository.GetById(request.Id)
            ?? throw new KeyNotFoundException($"Post with Id {request.Id} not found");
        postRepository.Remove(post);
    }
}
