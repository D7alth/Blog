using Blog.Application.Posts.Services;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Commands.Update;

public sealed class UpdateCommandHandler(
    IPostRepository postRepository,
    ITextProcessor textProcessor
) : IRequestHandler<UpdateCommand>
{
    public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var post =
            await postRepository.GetById(request.Id)
            ?? throw new KeyNotFoundException($"Post with Id {request.Id} not found");
        post.Update(request.Title, textProcessor.SanitizeMarkdownToHtml(request.Content));
        postRepository.Update(post);
    }
}
