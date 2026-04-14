using System.Text.RegularExpressions;
using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

#pragma warning disable SYSLIB1045

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnsureStringTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        var value = "";
        var result = Ensure.NotEmpty(value);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("NotEmpty");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBeNull();
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must not be empty.");
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenValid()
    {
        var value = "abc";
        var result = Ensure.NotEmpty(value);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFailure_WhenWhitespace()
    {
        var value = " \n";
        var result = Ensure.NotWhiteSpace(value);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("NotWhiteSpace");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBeNull();
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must not be whitespace.");
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnSuccess_WhenValid()
    {
        var value = "abc";
        var result = Ensure.NotWhiteSpace(value);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void MinLength_Should_ReturnFailure_WhenShort()
    {
        var value = "abc";
        var result = Ensure.MinLength(value, 4);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("MinLength");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(4);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must be at least 4 characters.");
    }

    [Test]
    public void MinLength_Should_ReturnSuccess_WhenValid()
    {
        var value = "abc";
        var result = Ensure.MinLength(value, 3);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void MaxLength_Should_ReturnFailure_WhenLong()
    {
        var value = "abc";
        var result = Ensure.MaxLength(value, 2);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("MaxLength");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(2);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' must be at most 2 characters.");
    }

    [Test]
    public void MaxLength_Should_ReturnSuccess_WhenValid()
    {
        var value = "abc";
        var result = Ensure.MaxLength(value, 3);
        result.ShouldBeSuccess().ShouldBe(value);
    }

    [Test]
    public void Matches_Should_ReturnFailure_WhenNotMatching()
    {
        var regex = new Regex("^\\w$");
        var value = "abc def";
        var result = Ensure.Matches(value, regex);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("Matches");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBe(regex);
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe("'value' is not in the correct format.");
    }

    [Test]
    public void Matches_Should_ReturnSuccess_WhenValid()
    {
        var regex = new Regex("\\w");
        var value = "abc";
        var result = Ensure.Matches(value, regex);
        result.ShouldBeSuccess().ShouldBe(value);
    }
}