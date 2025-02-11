using Blog.Application.Posts.Services;
using Markdig;

namespace Blog.Infrastructure.Application.Posts;

public sealed class TextProcessor() : ITextProcessor
{
    public string Sanitize(string content)
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
