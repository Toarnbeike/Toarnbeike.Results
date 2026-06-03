using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class EnumGuardTests
{
    private enum TestEnum
    {
        Value1,
        Value2,
        Value3,
    };

    [Test]
    public void IsDefined_Should_ReturnTrue_WhenDefined()
    {
        EnumGuards.IsDefined(TestEnum.Value1).ShouldBeTrue();
    }

    [Test]
    public void IsDefined_Should_ReturnFalse_WhenNotDefined()
    {
        EnumGuards.IsDefined((TestEnum)100).ShouldBeFalse();
    }

    [Test]
    public void OneOf_Should_ReturnTrue_WhenContains()
    {
        EnumGuards.OneOf(TestEnum.Value1, [TestEnum.Value2, TestEnum.Value1]).ShouldBeTrue();
    }

    [Test]
    public void OneOf_Should_ReturnFalse_WhenNotContains()
    {
        EnumGuards.OneOf(TestEnum.Value1, [TestEnum.Value2, TestEnum.Value3]).ShouldBeFalse();
    }

    [Test]
    public void NotOneOf_Should_ReturnFalse_WhenContains()
    {
        EnumGuards.NotOneOf(TestEnum.Value1, [TestEnum.Value2, TestEnum.Value1]).ShouldBeFalse();
    }

    [Test]
    public void NotOneOf_Should_ReturnTrue_WhenNotContains()
    {
        EnumGuards.NotOneOf(TestEnum.Value1, [TestEnum.Value2, TestEnum.Value3]).ShouldBeTrue();
    }

}