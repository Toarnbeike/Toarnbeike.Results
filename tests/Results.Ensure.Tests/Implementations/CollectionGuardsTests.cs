using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class CollectionGuardsTests
{
    private readonly List<int> _empty = [];
    private readonly List<int> _notEmpty = [1, 2, 3];
    private readonly IEnumerable<int> _emptyNotAnICollection = [];
    private readonly IEnumerable<int> _notEmptyNotAnICollection = [1, 2, 3];

    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        var result = CollectionGuards.NotEmpty(_empty);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        var result = CollectionGuards.NotEmpty(_notEmpty);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Empty_Should_ReturnFailure_WhenNotEmpty()
    {
        var result = CollectionGuards.Empty(_notEmpty);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void Empty_Should_ReturnSuccess_WhenEmpty()
    {
        var result = CollectionGuards.Empty(_empty);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnFailure_WhenCountLessThanMin()
    {
        var result = CollectionGuards.AtLeast(_notEmpty, 5);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(5);
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenCountEqualsMin()
    {
        var result = CollectionGuards.AtLeast(_notEmpty, 3);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenCountGreaterThanMax()
    {
        var result = CollectionGuards.AtMost(_notEmpty, 2);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(2);
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenCountEqualsMax()
    {
        var result = CollectionGuards.AtMost(_notEmpty, 3);
        result.IsValid.ShouldBeTrue();
    }

    // NotAnICollection tests

    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty_NotAnICollection()
    {
        var result = CollectionGuards.NotEmpty(_emptyNotAnICollection);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty_NotAnICollection()
    {
        var result = CollectionGuards.NotEmpty(_notEmptyNotAnICollection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Empty_Should_ReturnFailure_WhenNotEmpty_NotAnICollection()
    {
        var result = CollectionGuards.Empty(_notEmptyNotAnICollection);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void Empty_Should_ReturnSuccess_WhenEmpty_NotAnICollection()
    {
        var result = CollectionGuards.Empty(_emptyNotAnICollection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnFailure_WhenCountLessThanMin_NotAnICollection()
    {
        var result = CollectionGuards.AtLeast(_notEmptyNotAnICollection, 5);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(5);
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenCountEqualsMin_NotAnICollection()
    {
        var result = CollectionGuards.AtLeast(_notEmptyNotAnICollection, 3);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenCountGreaterThanMax_NotAnICollection()
    {
        var result = CollectionGuards.AtMost(_notEmptyNotAnICollection, 2);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(2);
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenCountEqualsMax_NotAnICollection()
    {
        var result = CollectionGuards.AtMost(_notEmptyNotAnICollection, 3);
        result.IsValid.ShouldBeTrue();
    }
}
