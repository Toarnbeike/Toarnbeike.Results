using Toarnbeike.Optional;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Optional.Tests;

/// <summary>
/// Tests for the <see cref="ToResultExtensions"/> class.
/// </summary>
public class ToResultExtensionsTests
{
    private readonly Option<int> _some = Option.Some(3);
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someTask = Task.FromResult(Option.Some(3));
    private readonly Task<Option<int>> _noneTask = Task.FromResult(Option<int>.None());

    private readonly Func<Failure> _failureProvider = () => new Failure("test", "Test failure");
    private readonly Func<Task<Failure>> _failureProviderAsync = () => Task.FromResult(new Failure("test", "Test failure"));

    [Fact]
    public void ToResult_ShouldReturnSuccess_WhenOptionIsSome()
    {
        var result = _some.ToResult(_failureProvider);
        result.ShouldBeSuccessWithValue(3);
    }

    [Fact]
    public void ToResult_ShouldReturnFailure_WhenOptionIsNone()
    {
        var result = _none.ToResult(_failureProvider);
        result.ShouldBeFailureWithCode("test");
    }

    [Fact]
    public async Task ToResultAsync_ShouldReturnSuccess_WhenOptionIsSome()
    {
        var result = await _some.ToResultAsync(_failureProviderAsync);
        result.ShouldBeSuccessWithValue(3);
    }

    [Fact]
    public async Task ToResultAsync_ShouldReturnFailure_WhenOptionIsNone()
    {
        var result = await _none.ToResultAsync(_failureProviderAsync);
        result.ShouldBeFailureWithCode("test");
    }

    [Fact]
    public async Task ToResult_ShouldReturnSuccess_WhenOptionTaskIsSome()
    {
        var result = await _someTask.ToResult(_failureProvider);
        result.ShouldBeSuccessWithValue(3);
    }

    [Fact]
    public async Task ToResult_ShouldReturnFailure_WhenOptionTaskIsNone()
    {
        var result = await _noneTask.ToResult(_failureProvider);
        result.ShouldBeFailureWithCode("test");
    }

    [Fact]
    public async Task ToResultAsync_ShouldReturnSuccess_WhenOptionTaskIsSome()
    {
        var result = await _someTask.ToResultAsync(_failureProviderAsync);
        result.ShouldBeSuccessWithValue(3);
    }

    [Fact]
    public async Task ToResultAsync_ShouldReturnFailure_WhenOptionTaskIsNone()
    {
        var result = await _noneTask.ToResultAsync(_failureProviderAsync);
        result.ShouldBeFailureWithCode("test");
    }
}
