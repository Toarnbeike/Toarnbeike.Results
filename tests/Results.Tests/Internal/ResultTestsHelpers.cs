namespace Toarnbeike.Results.Tests.Internal;

internal static class ResultTestsHelpers
{
    public static void ShouldBeSuccessWithValue<TValue>(this Result<TValue> result, TValue expected) 
    {
        result.IsSuccess.ShouldBeTrue();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(expected);
    }

    public static void ShouldBeFailureWithCodeAndMessage(this IResult result, string expectedCode, string expectedMessage)
    {
        result.IsFailure.ShouldBeTrue();
        result.TryGetFailure(out var failure).ShouldBeTrue();
        failure.Code.ShouldBe(expectedCode);
        failure.Message.ShouldBe(expectedMessage);
    }

    public static TFailure ShouldBeFailureOfType<TFailure>(this IResult result)
        where TFailure : Failure
    {
        result.TryGetFailure(out var failure).ShouldBeTrue();
        var returnValue = failure as TFailure;
        returnValue.ShouldNotBeNull();
        return returnValue;
    }
}