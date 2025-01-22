namespace Blog.Application.Posts.Services;

public interface ITextProcessor
{
    public string Sanitize(string text);
}
