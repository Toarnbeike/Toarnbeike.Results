using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.UtcNow;

namespace Toarnbeike.Rules.Tests.UtcNowTests;

public class StaticUtcNowProviderTests
{
    [Test]
    public void GetUtcNow_ShouldReturnFixedValue_WhenUsingStaticUtcNowProvider()
    {
        var fixedUtcNow = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var staticProvider = new StaticUtcNowProvider(fixedUtcNow);

        var dateTimeResult = staticProvider.GetUtcNow<DateTime>();
        dateTimeResult.ShouldBe(fixedUtcNow.DateTime);

        var dateTimeOffsetResult = staticProvider.GetUtcNow<DateTimeOffset>();
        dateTimeOffsetResult.ShouldBe(fixedUtcNow);

        var dateOnlyResult = staticProvider.GetUtcNow<DateOnly>();
        dateOnlyResult.ShouldBe(DateOnly.FromDateTime(fixedUtcNow.DateTime));

        var timeOnlyResult = staticProvider.GetUtcNow<TimeOnly>();
        timeOnlyResult.ShouldBe(TimeOnly.FromDateTime(fixedUtcNow.DateTime));
    }

    [Test]
    public void GetUtcNow_ShouldThrowNotSupportedException_WhenUsingUnsupportedType()
    {
        var fixedUtcNow = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var staticProvider = new StaticUtcNowProvider(fixedUtcNow);
        Should.Throw<NotSupportedException>(() => staticProvider.GetUtcNow<int>());
    }

    [Test, NotInParallel]
    public void EndToEndTest_WithStaticUtcNowProvider()
    {
        var fixedUtcNow = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

        // Check if the RuleContext is using the StaticUtcNowProvider and returns the fixed value
        RuleContext.UseFixedUtcNow(fixedUtcNow);
        var utcNowFromContext = RuleContext.UtcNow<DateTimeOffset>();
        utcNowFromContext.ShouldBe(fixedUtcNow);

        // Check if the DateRules.Past rule correctly evaluates against the fixed UTC time
        var ruleResult = DateRules.Past<DateTimeOffset>()(new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero));
        ruleResult.IsValid.ShouldBeFalse();
        ruleResult.Context.ShouldContainKeyAndValue("max", fixedUtcNow);
    }
}