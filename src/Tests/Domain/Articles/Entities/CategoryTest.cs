using Blog.Domain.Articles.Entities;

namespace Blog.Tests.Domain.Articles.Entities;

[TestFixture]
public class CategoryTest
{
    private const string CategoryName = "Books";
    private const string CategoryNameWithSpaces = "Clean Code";
    private const string CategoryNameWithSpecialChars = "C# & .NET";
    private const string CategoryDescription = "Any Description";

    [Test]
    [TestCase(CategoryName)]
    [TestCase(CategoryNameWithSpaces)]
    [TestCase(CategoryNameWithSpecialChars)]
    public void ShouldCreateCategory(string categoryName) =>
        Assert.That(
            Category.Create(categoryName, CategoryDescription, false).Name,
            Is.EqualTo(categoryName)
        );

    [Test]
    [TestCase(CategoryName)]
    [TestCase(CategoryNameWithSpaces)]
    [TestCase(CategoryNameWithSpecialChars)]
    public void ShouldApplySlugToName(string categoryName) =>
        Assert.That(
            Category.Create(categoryName, CategoryDescription, false).Slug,
            Is.EqualTo(categoryName.ToLower().Replace(" ", "-"))
        );

    [Test]
    public void ShouldNotCreateDuplicateCategory() =>
        Assert.Throws<ArgumentException>(
            () => Category.Create(CategoryName, CategoryDescription, true)
        );
}
