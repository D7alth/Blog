using Blog.PostContext.Domain.Articles.Entities;
using MediatR;

namespace Blog.PostContext.Application.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IRequest<IEnumerable<Category>>;
