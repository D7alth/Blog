using Blog.Application.Categories.Commands.CreateCategory;
using Carter;
using FluentValidation;
using MediatR;

namespace Blog.API.Endpoints.Categories;

public class CategoryModule() : CarterModule("/api/categories")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
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
