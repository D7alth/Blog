using Blog.Domain.Articles.Entities;
using Blog.Domain.Articles.Repositories;
using MediatR;

namespace Blog.Application.Categories.Commands.CreateCategory;

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
