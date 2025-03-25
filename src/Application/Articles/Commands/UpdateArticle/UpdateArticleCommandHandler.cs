using Blog.Application.Articles.Services;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.UpdateArticle;

public sealed class UpdateArticleCommandHandler(
    IPostRepository postRepository,
    ITextProcessor textProcessor
) : IRequestHandler<UpdateArticleCommand>
{
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var post =
            await postRepository.GetById(request.Id)
            ?? throw new KeyNotFoundException($"Post with Id {request.Id} not found");
        post.Update(request.Title, textProcessor.SanitizeMarkdownToHtml(request.Content));
        postRepository.Update(post);
    }
}
