using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class GuidExtensionTests
{
    private readonly Guid _empty = Guid.Empty;
    private readonly Guid _version4 = Guid.NewGuid();
    private readonly Guid _version7 = Guid.CreateVersion7();

    private readonly string _customMessage = "CustomFailureMessage";

    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        var result = _empty.NotEmpty();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("NotEmpty");
        failure.Message.ShouldBe($"'{nameof(_empty)}' must not be an empty Guid, but is.");
    }

    [Test]
    public void NotEmpty_Should_ReturnCustomMessageFailure()
    {
        var result = _empty.NotEmpty(_customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }

    [Test]
    public void IsVersion4_Should_ReturnFormattedFailure()
    {
        var result = _version7.IsVersion4();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("IsVersion4");
        failure.Message.ShouldBe($"'{nameof(_version7)}' must be Guid_v4, but is v7.");
    }

    [Test]
    public void IsVersion4_Should_ReturnCustomMessageFailure()
    {
        var result = _version7.IsVersion4(_customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }

    [Test]
    public void IsVersion7_Should_ReturnFormattedFailure()
    {
        var result = _version4.IsVersion7();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("IsVersion7");
        failure.Message.ShouldBe($"'{nameof(_version4)}' must be Guid_v7, but is v4.");
    }

    [Test]
    public void IsVersion7_Should_ReturnCustomMessageFailure()
    {
        var result = _version4.IsVersion7(_customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }
}