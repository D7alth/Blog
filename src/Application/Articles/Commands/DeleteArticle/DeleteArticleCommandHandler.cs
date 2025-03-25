using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.DeleteArticle;

public sealed class DeleteArticleCommandHandler(IPostRepository postRepository)
    : IRequestHandler<DeleteArticleCommand>
{
    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var post =
            await postRepository.GetById(request.Id)
            ?? throw new KeyNotFoundException($"Post with Id {request.Id} not found");
        postRepository.Remove(post);
    }
}
