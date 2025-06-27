using Blog.PostContext.Domain.Articles.Entities;
using Blog.PostContext.Domain.Articles.Repositories;
using MediatR;

namespace Blog.PostContext.Application.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken
    ) => await categoryRepository.GetCategoriesAsync();
}
