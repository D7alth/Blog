using Blog.Application.Posts.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Commands.Create;

public sealed class CreateCommandHandler(IPostRepository postRepository, ITextProcessor processor)
    : IRequestHandler<CreateCommand>
{
    public Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var sanitizerContent = processor.Sanitize(request.Content);
        var post = Post.Create(request.Title, sanitizerContent);
        postRepository.Add(post);
        return Task.CompletedTask;
    }
}
