namespace Blog.Application.Articles.Services;

public interface ITextProcessor
{
    public string SanitizeMarkdownToHtml(string text);
}
