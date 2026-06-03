using Toarnbeike.Results.Guards.Extensions;

namespace Toarnbeike.Results.Guards.Tests.Extensions;

public class PredicateExtensionTests
{
    private readonly int _value = 10;

    private static bool False(int value) => false;
    private static Task<bool> FalseAsync(int value) => Task.FromResult(false);
    private static bool True(int value) => true;
    private static Task<bool> TrueAsync(int value) => Task.FromResult(true);

    [Test]
    public void Satisfies_Should_ReturnFormattedFailure()
    {
        var result = _value.Satisfies(False);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must satisfy a given condition, but does not.");
    }

    [Test]
    public void NotSatisfies_Should_ReturnFormattedFailure()
    {
        var result = _value.NotSatisfies(True);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must not satisfy a given condition, but does.");
    }

    [Test]
    public async Task SatisfiesAsync_Should_ReturnFormattedFailure()
    {
        var result = await _value.SatisfiesAsync(FalseAsync);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must satisfy a given condition, but does not.");
    }

    [Test]
    public async Task NotSatisfiesAsync_Should_ReturnFormattedFailure()
    {
        var result = await _value.NotSatisfiesAsync(TrueAsync);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must not satisfy a given condition, but does.");
    }
}