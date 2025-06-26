using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken
    ) => await categoryRepository.GetCategoriesAsync();
}
