
using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        => await categoryRepository.GetCategoryById(request.CategoryId) ?? null!;
}
