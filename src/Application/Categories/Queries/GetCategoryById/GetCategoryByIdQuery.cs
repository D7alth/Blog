using Blog.Domain.Articles.Entities;
using MediatR;

namespace Blog.Application.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(int CategoryId) : IRequest<Category>;
