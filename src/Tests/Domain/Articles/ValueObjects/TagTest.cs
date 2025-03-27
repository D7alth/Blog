using Blog.Domain.Articles.ValueObjects;

namespace Blog.Tests.Domain.Articles.ValueObjects;

[TestFixture]
public class TagTest
{
    private const string TagName = "Books";

    [Test]
    public void ShouldCreateTag()
    {
        var tag = Tag.Create(TagName);
        Assert.That(tag.Name, Is.EqualTo(TagName));
    }
}
