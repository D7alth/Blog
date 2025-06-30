using Blog.PostContext.Domain.Articles.Entities;
using MediatR;

namespace Blog.PostContext.Application.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(int CategoryId) : IRequest<Category>;
