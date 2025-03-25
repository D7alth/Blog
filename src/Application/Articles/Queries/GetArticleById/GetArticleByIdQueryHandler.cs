using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetArticleByIdQuery, ArticleResponse>
{
    public async Task<ArticleResponse> Handle(
        GetArticleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var post = await postRepository.GetById(request.Id);
        return new(post.Title!, post.Content!, post.Tags, post.CreatedAt, post.UpdatedAt);
    }
}
