namespace Blog.PostContext.Application.Articles.Services;

public interface ITextProcessor
{
    public string SanitizeMarkdownToHtml(string text);
}
