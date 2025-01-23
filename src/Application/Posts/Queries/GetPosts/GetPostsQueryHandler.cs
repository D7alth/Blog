using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPosts;

public sealed class GetPostsQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetPostsQuery, IEnumerable<PostsResponse>>
{
    public async Task<IEnumerable<PostsResponse>> Handle(
        GetPostsQuery request,
        CancellationToken cancellationToken
    )
    {
        var posts = await postRepository.GetAll();
        return posts.Select(post => new PostsResponse(
            post.Id,
            post.Title!,
            post.Content!,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }
}
