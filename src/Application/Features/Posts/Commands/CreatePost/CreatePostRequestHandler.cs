using Blog.Domain.Entities.Posts;
using Blog.Domain.Entities.Posts.Repositories;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.CreatePost;

public sealed class CreatePostRequestHandler(IPostRepository postRepository)
    : IRequestHandler<CreatePostRequest>
{
    public Task Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = Post.Create(request.Title, request.Content);
        postRepository.Add(post);
        return Task.CompletedTask;
    }
}
