using MediatR;

namespace Blog.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest;
