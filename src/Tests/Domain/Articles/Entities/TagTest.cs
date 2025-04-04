using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;

namespace Blog.Tests.Domain.Articles.Entities;

[TestFixture]
public class TagTest
{
    private const string TagName = "Books";
    private const string TagNameWithSpaces = "Clean Code";
    private const string TagNameWithSpecialChars = "C# & .NET";

    [Test]
    [TestCase(TagName)]
    [TestCase(TagNameWithSpaces)]
    [TestCase(TagNameWithSpecialChars)]
    public void ShouldCreateTag(string tagName) =>
        Assert.That(Tag.Create(tagName, false).Name, Is.EqualTo(tagName));

    [Test]
    [TestCase(TagName)]
    [TestCase(TagNameWithSpaces)]
    [TestCase(TagNameWithSpecialChars)]
    public void ShouldApplySlugToName(string tagName) =>
        Assert.That(
            Tag.Create(tagName, false).Slug,
            Is.EqualTo(tagName.ToLower().Replace(" ", "-"))
        );

    [Test]
    public void ShouldNotCreateDuplicateTag() =>
        Assert.Throws<ArgumentException>(() => Tag.Create(TagName, true));

    [Test]
    public void ShouldAddArticleToTag()
    {
        var tag = Tag.Create(TagName, false);
        var article = Article.Create("Test Article", "Test Content");
        tag.AddArticle(article);
        Assert.Multiple(() =>
        {
            Assert.That(tag.Articles, Has.Count.EqualTo(1));
            Assert.That(tag.Articles.First(), Is.EqualTo(article));
        });
    }
}
