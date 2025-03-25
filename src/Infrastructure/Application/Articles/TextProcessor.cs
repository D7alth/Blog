using Blog.Application.Articles.Services;
using Markdig;

namespace Blog.Infrastructure.Application.Articles;

public sealed class TextProcessor() : ITextProcessor
{
    public string SanitizeMarkdownToHtml(string content)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseAutoIdentifiers()
            .DisableHtml()
            .UseEmojiAndSmiley()
            .UseCitations()
            .UseDiagrams()
            .UseGridTables()
            .Build();
        var normalizedContent = Markdown.Normalize(content);
        var html = Markdown.ToHtml(normalizedContent, pipeline);
        return html;
    }
}
