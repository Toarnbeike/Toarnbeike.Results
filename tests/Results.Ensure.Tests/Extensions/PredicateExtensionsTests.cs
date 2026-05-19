//using Toarnbeike.Results.Ensure.Extensions;
//using Toarnbeike.Results.TestExtensions;

//namespace Toarnbeike.Results.Ensure.Tests.Extensions;

//public class PredicateExtensionsTests
//{
//    private readonly int _value = 10;
//    private readonly string _customMessage = "Custom failure message";

//    [Test]
//    public void Satisfies_Should_ReturnFormattedFailure()
//    {
//        var result = _value.Satisfies(False);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("Satisfies");
//        failure.Message.ShouldBe("Predicate on '_value' must be true.");
//    }

//    [Test]
//    public void Satisfies_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.Satisfies(False, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void DoesNotSatisfy_Should_ReturnFormattedFailure()
//    {
//        var result = _value.DoesNotSatisfy(True);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("DoesNotSatisfy");
//        failure.Message.ShouldBe("Predicate on '_value' must be false.");
//    }

//    [Test]
//    public void DoesNotSatisfy_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.DoesNotSatisfy(True, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public async Task SatisfiesAsync_Should_ReturnFormattedFailure()
//    {
//        var result = await _value.SatisfiesAsync(FalseAsync);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("SatisfiesAsync");
//        failure.Message.ShouldBe("Predicate on '_value' must be true.");
//    }

//    [Test]
//    public async Task SatisfiesAsync_Should_ReturnCustomMessageFailure()
//    {
//        var result = await _value.SatisfiesAsync(FalseAsync, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public async Task DoesNotSatisfyAsync_Should_ReturnFormattedFailure()
//    {
//        var result = await _value.DoesNotSatisfyAsync(TrueAsync);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("DoesNotSatisfyAsync");
//        failure.Message.ShouldBe("Predicate on '_value' must be false.");
//    }

//    [Test]
//    public async Task DoesNotSatisfyAsync_Should_ReturnCustomMessageFailure()
//    {
//        var result = await _value.DoesNotSatisfyAsync(TrueAsync, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    private static bool False(int value) => false;
//    private static Task<bool> FalseAsync(int value) => Task.FromResult(false);
//    private static bool True(int value) => true;
//    private static Task<bool> TrueAsync(int value) => Task.FromResult(true);
//}
