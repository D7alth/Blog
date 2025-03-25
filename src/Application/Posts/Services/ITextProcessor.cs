namespace Blog.Application.Posts.Services;

public interface ITextProcessor
{
    public string SanitizeMarkdownToHtml(string text);
}
