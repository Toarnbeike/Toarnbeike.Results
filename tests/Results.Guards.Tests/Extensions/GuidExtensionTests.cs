using Toarnbeike.Results.Guards.Extensions;

namespace Toarnbeike.Results.Guards.Tests.Extensions;

public class GuidExtensionTests
{
    private readonly Guid _empty = Guid.Empty;
    private readonly Guid _version4 = Guid.NewGuid();
    private readonly Guid _version7 = Guid.CreateVersion7();

    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        var result = _empty.NotEmpty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _empty,
            expectedMessage: "'_empty' must not be an empty Guid, but is.");
    }

    [Test]
    public void Version4_Should_ReturnFormattedFailure()
    {
        var result = _version7.Version4();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _version7,
            expectedMessage: "'_version7' must be a version 4 Guid, but is version 7.");
    }

    [Test]
    public void Version7_Should_ReturnFormattedFailure()
    {
        var result = _version4.Version7();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _version4,
            expectedMessage: "'_version4' must be a version 7 Guid, but is version 4.");
    }
}