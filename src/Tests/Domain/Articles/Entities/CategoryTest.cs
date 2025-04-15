using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;

namespace Blog.Tests.Domain.Articles.Entities;

[TestFixture]
public class CategoryTest
{
    private const string CategoryName = "Books";
    private const string CategoryNameWithSpaces = "Clean Code";
    private const string CategoryNameWithSpecialChars = "C# & .NET";

    [Test]
    [TestCase(CategoryName)]
    [TestCase(CategoryNameWithSpaces)]
    [TestCase(CategoryNameWithSpecialChars)]
    public void ShouldCreateCategory(string categoryName) =>
        Assert.That(Category.Create(categoryName, false).Name, Is.EqualTo(categoryName));

    [Test]
    [TestCase(CategoryName)]
    [TestCase(CategoryNameWithSpaces)]
    [TestCase(CategoryNameWithSpecialChars)]
    public void ShouldApplySlugToName(string categoryName) =>
        Assert.That(
            Category.Create(categoryName, false).Slug,
            Is.EqualTo(categoryName.ToLower().Replace(" ", "-"))
        );

    [Test]
    public void ShouldNotCreateDuplicateCategory() =>
        Assert.Throws<ArgumentException>(() => Category.Create(CategoryName, true));

    [Test]
    public void ShouldAddArticleToCategory()
    {
        var category = Category.Create(CategoryName, false);
        var article = Article.Create("Test Article", "Test Content");
        category.AddArticle(article);
        Assert.Multiple(() =>
        {
            Assert.That(category.Articles, Has.Count.EqualTo(1));
            Assert.That(category.Articles.First(), Is.EqualTo(article));
        });
    }
}
