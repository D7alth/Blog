using MediatR;

namespace Blog.PostContext.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest;
