namespace Blog.API.Endpoints.Articles;

public sealed record CreateRequest(string Title, string Content, List<string>? Tags);
