using Blog.Application.Features.Posts.Commands.CreatePost;
using Blog.Application.Features.Posts.Queries.GetPosts;
using Blog.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ServiceRegistration.AddInfrastructure(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost(
    "/api/posts",
    async (CreatePostRequest request, IMediator mediator) =>
    {
        try
        {
            await mediator.Send(request);
            return Results.Created();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }
);

app.MapGet(
    "/api/posts",
    async (IMediator mediator) =>
    {
        try
        {
            var posts = await mediator.Send(new GetPostsQuery());
            return Results.Ok(posts);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }
);

app.Run();
