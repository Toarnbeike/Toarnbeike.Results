using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class GuidTests
{
    [Test]
    public void NotEmpty_ShouldReturnSuccess_WhenGuidIsNotEmpty()
    {
        Result<Guid> result = Guid.NewGuid();
        var validationResult = result.TestRuleFor("Guid").NotEmpty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenGuidIsEmpty()
    {
        Result<Guid> result = Guid.Empty;
        var validationResult = result.TestRuleFor("Guid").NotEmpty();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailureWithCustomMessage_WhenGuidIsEmpty()
    {
        Result<Guid> result = Guid.Empty;
        var validationResult = result.TestRuleFor("Guid").NotEmpty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Version4_ShouldReturnSuccess_WhenGuidIsVersion4()
    {
        Result<Guid> result = Guid.NewGuid();
        var validationResult = result.TestRuleFor("Guid").Version4();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Version4_ShouldReturnFailure_WhenGuidIsNotVersion4()
    {
        Result<Guid> result = Guid.CreateVersion7();
        var validationResult = result.TestRuleFor("Guid").Version4();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Version4_ShouldReturnFailureWithCustomMessage_WhenGuidIsNotVersion4()
    {
        Result<Guid> result = Guid.CreateVersion7();
        var validationResult = result.TestRuleFor("Guid").Version4("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Version7_ShouldReturnSuccess_WhenGuidIsVersion7()
    {
        Result<Guid> result = Guid.CreateVersion7();
        var validationResult = result.TestRuleFor("Guid").Version7();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Version7_ShouldReturnFailure_WhenGuidIsNotVersion7()
    {
        Result<Guid> result = Guid.NewGuid();
        var validationResult = result.TestRuleFor("Guid").Version7();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Version7_ShouldReturnFailureWithCustomMessage_WhenGuidIsNotVersion7()
    {
        Result<Guid> result = Guid.NewGuid();
        var validationResult = result.TestRuleFor("Guid").Version7("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
