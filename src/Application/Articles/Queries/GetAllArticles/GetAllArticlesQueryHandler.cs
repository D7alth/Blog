using Blog.Domain.Posts.Repositories;
using MediatR;

namespace Blog.Application.Articles.Queries.GetAllArticles;

public sealed class GetAllArticlesQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleResponse>>
{
    public async Task<IEnumerable<ArticleResponse>> Handle(
        GetAllArticlesQuery request,
        CancellationToken cancellationToken
    )
    {
        var posts = await postRepository.GetAll();
        return posts.Select(post => new ArticleResponse(
            post.Id,
            post.Title!,
            post.Content!,
            post.Tags,
            post.CreatedAt,
            post.UpdatedAt
        ));
    }
}
