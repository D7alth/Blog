using Blog.Application.Posts.Commands.Create;
using Blog.Application.Posts.Commands.Delete;
using Blog.Application.Posts.Commands.Update;
using Blog.Application.Posts.Queries.GetAll;
using Blog.Application.Posts.Queries.GetById;
using Carter;
using MediatR;

namespace Blog.API.Endpoints.Posts;

public class PostModule() : CarterModule("/api/posts")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "{id:int}",
            async (int id, IMediator mediator) =>
            {
                var post = await mediator.Send(new GetByIdQuery(id));
                return Results.Ok(post);
            }
        );

        app.MapGet(
            "posts",
            async (IMediator mediator) =>
            {
                var posts = await mediator.Send(new GetAllQuery());
                return Results.Ok(posts);
            }
        );

        app.MapPost(
            "",
            async (CreateRequest request, IMediator mediator) =>
            {
                var command = new CreateCommand(request.Title, request.Content, request.Tags);
                await mediator.Send(command);
                return Results.Created();
            }
        );

        app.MapPut(
            "{id:int}",
            async (int id, UpdateRequest request, IMediator mediator) =>
            {
                var command = new UpdateCommand(id, request.Title, request.Content);
                await mediator.Send(command);
                return Results.NoContent();
            }
        );

        app.MapDelete(
            "{id:int}",
            async (int id, IMediator mediator) =>
            {
                var command = new DeleteCommand(id);
                await mediator.Send(command);
                return Results.NoContent();
            }
        );
    }
}
