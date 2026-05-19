//using Toarnbeike.Results.Ensure.Extensions;
//using Toarnbeike.Results.Ensure.Rules;
//using Toarnbeike.Results.Failures;
//using Toarnbeike.Results.TestExtensions;

using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

using Toarnbeike.Results.Ensure.Rules;

public class EnsureExtensionsTests
{
    //private readonly string _value = "Hello";

    [Test]
    public void Test()
    {
        var guid = Guid.NewGuid();
        List<string> letters = ["A", "B", "C"];
        var result = Result.Ensure()
            .That(letters).Between(2, 5)
            .That(guid).NotEmpty().WithMessage("Should not be empty")
            .That(guid).Version7().WithMessage("test123").WithArgumentName("somethingElse")
            .ToResult();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("test123");
        failure.ParameterName.ShouldBe("somethingElse");
    }

//    [Test]
//    public void Ensure_Should_ExtendStaticResult()
//    {
//        var result = Result.Ensure(() => _value.NotNullOrEmpty());
//        result.ShouldBeSuccess();
//    }

//    [Test]
//    public void Ensure_Should_ExtendStaticResult_ForAFuncResultDelegate()
//    {
//        var result = Result.Ensure(() => _value.Length > 0 ? Result.Success() : new SimpleFailure("Test", "Test"));
//        result.ShouldBeSuccess();
//    }

//    [Test]
//    public void Ensure_Should_ExtendStaticResult_WithMethodInvocation()
//    {
//        var result = Result.Ensure(CheckSomething);
//        result.ShouldBeSuccess();
//    }

//    [Test]
//    public void Ensure_Should_ExtendResult()
//    {
//        var result = Result.Success().Ensure(() => _value.NotNullOrEmpty());
//        result.ShouldBeSuccess();
//    }

//    [Test]
//    public void Ensure_Should_ExtendResult_ForAFuncResultDelegate()
//    {
//        var result = Result.Success().Ensure(() => _value.Length > 0 ? Result.Success() : new SimpleFailure("Test", "Test"));
//        result.ShouldBeSuccess();
//    }

//    [Test]
//    public void Ensure_Should_ExtendResult_WithMethodInvocation()
//    {
//        var result = Result.Success().Ensure(CheckSomething);
//        result.ShouldBeSuccess();
//    }

//    private Result CheckSomething()
//    {
//        return _value.Length > 0 ? Result.Success() : new SimpleFailure("Test", "Test");
//    }
}
