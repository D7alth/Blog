using Blog.Application.Categories.Commands.CreateCategory;
using FluentValidation.TestHelper;

namespace Blog.Tests.Application.Categories.Commands.CreateCategory;

[TestFixture]
public class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator = new();

    [Test]
    public void ShouldHaveNoErrorsWhenCategoryNameIsValid()
    {
        var command = new CreateCategoryCommand("Valid Tag", "Valid Description");
        _validator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldHaveErrorWhenCategoryNameIsEmpty()
    {
        var command = new CreateCategoryCommand("", "");
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Must be not empty");
    }

    [Test]
    public void ShouldHaveErrorWhenCategoryNameIsTooLong()
    {
        var command = new CreateCategoryCommand(new string('a', 51), "");
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Must be maximum Length is 50");
    }

    [Test]
    public void ShouldHaveErrorWhenCategoryNameIsTooShort()
    {
        var command = new CreateCategoryCommand("", "");
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Must be minimum Length is 1");
    }
}
