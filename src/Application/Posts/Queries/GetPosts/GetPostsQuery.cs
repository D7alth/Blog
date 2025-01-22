using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPosts;

public sealed record GetPostsQuery() : IRequest<IEnumerable<PostsResponse>>;
