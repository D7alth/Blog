using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Commands.DeleteArticle;
using Blog.Application.Articles.Commands.UpdateArticle;
using Blog.Application.Articles.Queries.GetAllArticles;
using Blog.Application.Articles.Queries.GetArticleById;
using Carter;
using MediatR;

namespace Blog.API.Endpoints.Articles;

public class ArticleModule() : CarterModule("/api/articles")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "{id:int}",
            async (int id, IMediator mediator) =>
            {
                var article = await mediator.Send(new GetArticleByIdQuery(id));
                return Results.Ok(article);
            }
        );

        app.MapGet(
            "",
            async (IMediator mediator) =>
            {
                var articles = await mediator.Send(new GetAllArticlesQuery());
                return Results.Ok(articles);
            }
        );

        app.MapPost(
            "",
            async (CreateRequest request, IMediator mediator) =>
            {
                var command = new CreateArticleCommand(
                    request.Title,
                    request.Content,
                    request.Tags
                );
                await mediator.Send(command);
                return Results.Created();
            }
        );

        app.MapPut(
            "{id:int}",
            async (int id, UpdateRequest request, IMediator mediator) =>
            {
                var command = new UpdateArticleCommand(id, request.Title, request.Content);
                await mediator.Send(command);
                return Results.NoContent();
            }
        );

        app.MapDelete(
            "{id:int}",
            async (int id, IMediator mediator) =>
            {
                var command = new DeleteArticleCommand(id);
                await mediator.Send(command);
                return Results.NoContent();
            }
        );
    }
}
