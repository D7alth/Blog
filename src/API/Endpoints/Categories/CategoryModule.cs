using Blog.Application.Categories.Commands.CreateCategory;
using Blog.Application.Categories.Queries.GetCategoryById;
using Carter;
using FluentValidation;
using MediatR;

namespace Blog.API.Endpoints.Categories;

public class CategoryModule() : CarterModule("/api/categories")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "{categoryId:int}",
            async (
                int categoryId,
                IMediator mediator,
                IValidator<GetCategoryByIdQuery> validator
            ) =>
            {
                var query = new GetCategoryByIdQuery(categoryId);
                var validationResult = validator.Validate(query);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            }
        );
        app.MapPost(
            "",
            async (
                CreateCategoryRequest request,
                IMediator mediator,
                IValidator<CreateCategoryCommand> validator
            ) =>
            {
                var command = new CreateCategoryCommand(request.CategoryName, request.Description);
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                await mediator.Send(command);
                return Results.Ok();
            }
        );
    }
}
