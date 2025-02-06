using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Queries.GetById;

public sealed class GetByIdQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetByIdQuery, PostResponse>
{
    public async Task<PostResponse> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var post = await postRepository.GetById(request.Id);
        return new(post.Title!, post.Content!, post.CreatedAt, post.UpdatedAt);
    }
}
