using Blog.Application.Posts.Commands.Create;
using Blog.Application.Posts.Queries.GetAll;
using Carter;
using MediatR;

namespace Blog.API.Endpoints.Posts;

public class PostModule() : CarterModule("/api/post")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "",
            async (CreateRequest request, IMediator mediator) =>
            {
                var command = new CreateCommand(request.Title, request.Content);
                await mediator.Send(command);
                return Results.Created();
            }
        );

        app.MapGet(
            "",
            async (IMediator mediator) =>
            {
                var posts = await mediator.Send(new GetAllQuery());
                return Results.Ok(posts);
            }
        );
    }
}
