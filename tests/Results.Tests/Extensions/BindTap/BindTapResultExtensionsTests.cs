using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Extensions.BindTap;

/// <summary>
/// Tests for the <see cref="BindTapExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class BindTapResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly Func<Result> _successFunc = Result.Success;
    private readonly Func<Result> _failureFunc = () => Result.Failure(new Failure("BindTap", "BindTap failure"));
    private readonly Func<Result> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Result<int>> _successOfTFunc = () => Result.Success(42);
    private readonly Func<Result<int>> _failureOfTFunc = () => Result<int>.Failure(new Failure("BindTap", "BindTap failure"));
    private readonly Func<Result<int>> _forbiddenOfTFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result>> _successTaskFunc = () => Task.FromResult(Result.Success());
    private readonly Func<Task<Result>> _failureTaskFunc = () => Task.FromResult(Result.Failure(new Failure("BindTap", "BindTap failure")));
    private readonly Func<Task<Result>> _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result<int>>> _successOfTTaskFunc = () => Task.FromResult(Result<int>.Success(42));
    private readonly Func<Task<Result<int>>> _failureOfTTaskFunc = () => Task.FromResult(Result<int>.Failure(new Failure("BindTap", "BindTap failure")));
    private readonly Func<Task<Result<int>>> _forbiddenOfTTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.BindTap(_failureFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.BindTap(_forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = _success.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = _success.BindTap(_failureOfTFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _success.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = await _success.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTap(_failureFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTap(_forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTap(_failureOfTFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("original");
    }
}
