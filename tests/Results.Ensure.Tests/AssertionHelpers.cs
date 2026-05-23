using Toarnbeike.Results.Ensure.Implementation.RuleResults;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

internal static class AssertionHelpers
{
    extension<T>(Result<T> result)
    {
        public GuardFailure ShouldBeGuardFailure(T expectedAttemptedValue,
            string expectedMessage)
        {
            var failure = result.ShouldBeFailureOfType<GuardFailure>();
            failure.AttemptedValue.ShouldBe(expectedAttemptedValue);
            failure.Message.ShouldBe(expectedMessage);
            return failure;
        }
    }

    public static FailingRuleResult<T> AssertFailure<T>(this IGuardRuleResult result,
        T expectedAttemptedValue,
        string expectedArgumentName,
        string expectedGuardName,
        params (string Key, object? Value)[] expectedRuleContextElements)
    {
        var convertedResult = result as FailingRuleResult<T>;
        convertedResult.ShouldNotBeNull();
        convertedResult.ArgumentName.ShouldBe(expectedArgumentName);
        convertedResult.AttemptedValue.ShouldBe(expectedAttemptedValue);
        convertedResult.GuardName.ShouldBe(expectedGuardName);

        foreach (var (key, value) in expectedRuleContextElements)
        {
            var actualValue = convertedResult.Context.Get<object?>(key);
            actualValue.ShouldBe(value);
        }

        return convertedResult;
    }
}