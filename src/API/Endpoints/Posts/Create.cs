using Blog.Application.Posts.Commands.Create;
using MediatR;

namespace Blog.API.Endpoints.Posts;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/posts",
            async (CreateRequest request, IMediator mediator) =>
            {
                var command = new CreateCommand(request.Title, request.Content);
                await mediator.Send(command);
                return Results.Created();
            }
        );
    }
}
