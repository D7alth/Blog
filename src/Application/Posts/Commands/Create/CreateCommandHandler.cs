using Blog.Application.Posts.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Commands.Create;

public sealed class CreateCommandHandler(
    IPostRepository postRepository,
    ITextProcessor textProcessor
) : IRequestHandler<CreateCommand>
{
    public Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var post = Post.Create(
            request.Title,
            textProcessor.SanitizeMarkdownToHtml(request.Content),
            request.Tags
        );
        postRepository.Add(post);
        return Task.CompletedTask;
    }
}
