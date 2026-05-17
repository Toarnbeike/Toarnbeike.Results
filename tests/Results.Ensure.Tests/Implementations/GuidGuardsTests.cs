using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class GuidGuardsTests
{
    private readonly Guid _empty = Guid.Empty;
    private readonly Guid _version4 = Guid.NewGuid();
    private readonly Guid _version7 = Guid.CreateVersion7();

    [Test]
    public void NotEmpty_Should_ReturnFalse_WhenEmpty()
    {
        GuidGuards.NotEmpty(_empty).ShouldBeFalse();
    }

    [Test]
    public void NotEmpty_Should_ReturnTrue_WhenNotEmpty()
    {
        GuidGuards.NotEmpty(_version4).ShouldBeTrue();
    }

    [Test]
    public void Version_Should_ReturnFalse_WhenEmpty()
    {
        GuidGuards.IsVersion(_empty, 4).ShouldBeFalse();
    }

    [Test]
    public void Version_Should_ReturnFalse_WhenWrongVariant()
    {
        GuidGuards.IsVersion(_version7, 4).ShouldBeFalse();
    }

    [Test]
    public void Version_Should_ReturnTrue_WhenCorrectVariant()
    {
        GuidGuards.IsVersion(_version4, 4).ShouldBeTrue();
    }
}