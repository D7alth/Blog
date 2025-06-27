using Blog.PostContext.Domain.Articles.Repositories;
using MediatR;

namespace Blog.PostContext.Application.Articles.Commands.DeleteArticle;

public sealed class DeleteArticleCommandHandler(IArticleRepository articleRepository)
    : IRequestHandler<DeleteArticleCommand>
{
    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetById(request.Id);
        articleRepository.Remove(article);
    }
}
