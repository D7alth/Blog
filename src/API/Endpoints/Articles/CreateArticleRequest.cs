namespace Blog.API.Endpoints.Articles;

public sealed record CreateArticleRequest(string Title, string Content, int CategoryId = 1);
