using Blog.Application.Articles.Services;
using Blog.Domain.Posts;
using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandHandler(
    IPostRepository postRepository,
    ITextProcessor textProcessor
) : IRequestHandler<CreateArticleCommand>
{
    public Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var post = Post.Create(
            request.Title,
            textProcessor.SanitizeMarkdownToHtml(request.Content),
            request.Tags
        );
        postRepository.Add(post);
        return Task.CompletedTask;
    }
}
