using Blog.PostContext.Domain.Articles.Entities;
using Blog.PostContext.Domain.Articles.Repositories;
using MediatR;

namespace Blog.PostContext.Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isDuplicated = await categoryRepository.ExistsAsync(request.Name);
        var category = Category.Create(request.Name, request.Description, isDuplicated);
        categoryRepository.Add(category);
    }
}
