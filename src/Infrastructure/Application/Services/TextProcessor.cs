using Blog.Application.Posts.Services;
using Markdig;

namespace Blog.Infrastructure.Application.Entities.Posts.Services;

public sealed class TextProcessor() : ITextProcessor
{
    public string Sanitize(string content) => Markdown.ToHtml(Markdown.Normalize(content));
}
