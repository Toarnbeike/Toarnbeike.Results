using Toarnbeike.Results.Guards.Rules.Generated;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Guards.Tests.Rules.CollectionRules;

public class DateOnlyCollectionRuleTests
{
    [Test]
    public void ThatEach_OnOrAfter()
    {
        var listOfDates = new List<DateOnly>
        {
            new(2026, 1, 1),
            new(2026, 1, 2),
            new(2025,1,3),
            new(2025,1,4)
        };

        var result = Result.Validate().ThatEach(listOfDates)
            .OnOrAfter(new DateOnly(2026, 1, 1))
            .WithExpression("DateList")
            .WithMessage(expr => $"{expr} Should be in 2026")
            .ToResult("abc");
        result.ShouldBeFailure();
    }
}
