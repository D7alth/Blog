using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Commands.DeleteArticle;
using Blog.Application.Articles.Commands.UpdateArticle;
using Blog.Application.Articles.Queries.GetArticleById;
using Blog.Application.Articles.Queries.GetArticles;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            async (
                IMediator mediator,
                [FromQuery] DateTime? startDate,
                [FromQuery] DateTime? endDate,
                [FromQuery] int limit = 10,
                [FromQuery] int page = 1
            ) =>
            {
                var articles = await mediator.Send(
                    new GetArticlesQuery(startDate, endDate, limit, page)
                );
                return Results.Ok(articles);
            }
        );

        app.MapPost(
            "",
            async (
                CreateRequest request,
                IMediator mediator,
                IValidator<CreateArticleCommand> validator
            ) =>
            {
                var command = new CreateArticleCommand(
                    request.Title,
                    request.Content,
                    request.Tags
                );
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
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
            async (int id, IMediator mediator, IValidator<DeleteArticleCommand> validator) =>
            {
                var command = new DeleteArticleCommand(id);
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                await mediator.Send(command);
                return Results.NoContent();
            }
        );
    }
}
