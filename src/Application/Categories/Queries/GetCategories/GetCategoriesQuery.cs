using Blog.Domain.Articles.Entities;
using MediatR;

namespace Blog.Application.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IRequest<IEnumerable<Category>>;
