using MediatR;

namespace Blog.Application.Posts.Queries.GetById;

public sealed record GetByIdQuery(int Id) : IRequest<PostResponse>;
