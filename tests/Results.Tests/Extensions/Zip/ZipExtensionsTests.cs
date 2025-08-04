using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Extensions.Zip;
/// <summary>
/// Tests for the <see cref="ZipExtensions"/> on a <see cref="Result{T1}"/>.
/// </summary>
public class ZipExtensionsTests
{
    private readonly Result<double> _success = Result<double>.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, Result<int>> _secondSuccessFunc = value => (int)value;
    private readonly Func<double, Result<int>> _secondFailureFunc = _ => Result<int>.Failure(new Failure("zip", "Zip failure"));
    private readonly Func<double, Result<int>> _secondForbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");
    
    private readonly Func<double, Task<Result<int>>> _secondSuccessAsyncFunc = value => Task.FromResult(Result<int>.Success((int)value));
    private readonly Func<double, Task<Result<int>>> _secondFailureAsyncFunc = _ => Task.FromResult(Result<int>.Failure(new Failure("zip", "Zip failure")));
    private readonly Func<double, Task<Result<int>>> _secondForbiddenAsyncFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Zip_Should_ReturnTupleResult_WhenFirstResultIsSuccess_AndSecondResultIsSuccess()
    {
        var result = _success.Zip(_secondSuccessFunc);
        result.ShouldBeSuccessWithValue((1.3, 1));
    }

    [Fact]
    public void Zip_Should_ReturnSecondFailure_WhenFirstResultIsSuccess_AndSecondResultIsFailure()
    {
        var result = _success.Zip(_secondFailureFunc);
        result.ShouldBeFailureWithCodeAndMessage("zip", "Zip failure");
    }

    [Fact]
    public void Zip_Should_ReturnFirstFailure_WhenFirstResultIsFailure()
    {
        var result = _failure.Zip(_secondForbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnTupleResult_WhenFirstResultIsSuccess_AndSecondResultTaskIsSuccess()
    {
        var result = await _success.ZipAsync(_secondSuccessAsyncFunc);
        result.ShouldBeSuccessWithValue((1.3, 1));
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnSecondFailure_WhenFirstResultIsSuccess_AndSecondResultTaskIsFailure()
    {
        var result = await _success.ZipAsync(_secondFailureAsyncFunc);
        result.ShouldBeFailureWithCodeAndMessage("zip", "Zip failure");
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnFirstFailure_WhenFirstResultIsFailure()
    {
        var result = await _failure.ZipAsync(_secondForbiddenAsyncFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Zip_Should_ReturnTupleResult_WhenFirstResultTaskIsSuccess_AndSecondResultIsSuccess()
    {
        var result = await _successTask.Zip(_secondSuccessFunc);
        result.ShouldBeSuccessWithValue((1.3, 1));
    }

    [Fact]
    public async Task Zip_Should_ReturnSecondFailure_WhenFirstResultTaskIsSuccess_AndSecondResultIsFailure()
    {
        var result = await _successTask.Zip(_secondFailureFunc);
        result.ShouldBeFailureWithCodeAndMessage("zip", "Zip failure");
    }

    [Fact]
    public async Task Zip_Should_ReturnFirstFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _failureTask.Zip(_secondForbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnTupleResult_WhenFirstResultTaskIsSuccess_AndSecondResultTaskIsSuccess()
    {
        var result = await _successTask.ZipAsync(_secondSuccessAsyncFunc);
        result.ShouldBeSuccessWithValue((1.3, 1));
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnSecondFailure_WhenFirstResultTaskIsSuccess_AndSecondResultTaskIsFailure()
    {
        var result = await _successTask.ZipAsync(_secondFailureAsyncFunc);
        result.ShouldBeFailureWithCodeAndMessage("zip", "Zip failure");
    }

    [Fact]
    public async Task ZipAsync_Should_ReturnFirstFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _failureTask.ZipAsync(_secondForbiddenAsyncFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
