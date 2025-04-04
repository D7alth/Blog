using Blog.Application.Tags.Commands.CreateTag;
using FluentValidation.TestHelper;

namespace Blog.Tests.Application.Tags.Commands.CreateTag;

[TestFixture]
public class CreateTagCommandValidatorTests
{
    private readonly CreateTagCommandValidator _validator = new();

    [Test]
    public void ShouldHaveNoErrorsWhenTagNameIsValid()
    {
        var command = new CreateTagCommand("Valid Tag");
        _validator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void ShouldHaveErrorWhenTagNameIsEmpty()
    {
        var command = new CreateTagCommand("");
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.TagName)
            .WithErrorMessage("Must be not empty");
    }

    [Test]
    public void ShouldHaveErrorWhenTagNameIsTooLong()
    {
        var command = new CreateTagCommand(new string('a', 51));
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.TagName)
            .WithErrorMessage("Must be maximum Length is 50");
    }

    [Test]
    public void ShouldHaveErrorWhenTagNameIsTooShort()
    {
        var command = new CreateTagCommand("");
        _validator
            .TestValidate(command)
            .ShouldHaveValidationErrorFor(x => x.TagName)
            .WithErrorMessage("Must be minimum Length is 1");
    }
}
