using Blog.PostContext.Domain.Articles.Entities;
using Blog.PostContext.Domain.Articles.Repositories;
using MediatR;

namespace Blog.PostContext.Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken
    ) => await categoryRepository.GetCategoryById(request.CategoryId) ?? null!;
}
