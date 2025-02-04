using Blog.Application.Posts.Queries.GetAll;
using MediatR;

namespace Blog.API.Endpoints.Posts;

public sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/posts",
            async (IMediator mediator) =>
            {
                var posts = await mediator.Send(new GetAllQuery());
                return Results.Ok(posts);
            }
        );
    }
}
