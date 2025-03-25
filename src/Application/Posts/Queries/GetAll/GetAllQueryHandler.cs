using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Posts.Queries.GetAll;

public sealed class GetAllQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetAllQuery, IEnumerable<PostResponse>>
{
    public async Task<IEnumerable<PostResponse>> Handle(
        GetAllQuery request,
        CancellationToken cancellationToken
    )
    {
        var posts = await postRepository.GetAll();
        return posts.Select(post => new PostResponse(
            post.Id,
            post.Title!,
            post.Content!,
            post.Tags,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }
}
