using Blog.Application.Tags.Commands.CreateTag;
using Carter;
using FluentValidation;
using MediatR;

namespace Blog.API.Endpoints.Tags;

public class TagModule() : CarterModule("/api/tags")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "",
            async (
                CreateTagRequest request,
                IMediator mediator,
                IValidator<CreateTagCommand> validator
            ) =>
            {
                var command = new CreateTagCommand(request.TagName);
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                await mediator.Send(command);
                return Results.Ok();
            }
        );
    }
}
