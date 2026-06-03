using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Guards.Tests;

internal static class AssertionHelpers
{
    extension<T>(Result<T> result)
    {
        public GuardFailure ShouldBeGuardFailure(T expectedAttemptedValue,
            string expectedMessage)
        {
            var failure = result.ShouldBeFailureOfType<GuardFailure>();
            failure.RuleContext.AttemptedValue.ShouldBe(expectedAttemptedValue);
            failure.Message.ShouldBe(expectedMessage);
            return failure;
        }
    }

    public static IGuardRuleResult<T> AssertFailure<T>(this IGuardRuleResult result,
        T expectedAttemptedValue,
        string expectedArgumentName,
        string expectedGuardName,
        params (string Key, object? Value)[] expectedRuleContextElements)
    {
        var convertedResult = result as IGuardRuleResult<T>;
        convertedResult.ShouldNotBeNull();
        convertedResult.CapturedExpression.ShouldBe(expectedArgumentName);
        convertedResult.RuleContext.AttemptedValue.ShouldBe(expectedAttemptedValue);
        convertedResult.RuleContext.RuleName.ShouldBe(expectedGuardName);

        foreach (var (key, value) in expectedRuleContextElements)
        {
            var actualValue = convertedResult.RuleContext.Get<object?>(key);
            actualValue.ShouldBe(value);
        }

        return convertedResult;
    }
}