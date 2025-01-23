using Blog.Application.Posts.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Commands.CreatePost;

public sealed class CreatePostRequestHandler(
    IPostRepository postRepository,
    ITextProcessor processor
) : IRequestHandler<CreatePostRequest>
{
    public Task Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var sanitizerContent = processor.Sanitize(request.Content);
        var post = Post.Create(request.Title, sanitizerContent);
        postRepository.Add(post);
        return Task.CompletedTask;
    }
}
