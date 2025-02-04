using Blog.Application.Features.Posts.Queries.GetPosts;
using MediatR;

namespace Blog.Application.Posts.Queries.GetAll;

public sealed record GetAllQuery() : IRequest<IEnumerable<PostResponse>>;
