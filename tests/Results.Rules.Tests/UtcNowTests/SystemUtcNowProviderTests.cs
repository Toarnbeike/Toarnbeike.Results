using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.UtcNow;

namespace Toarnbeike.Rules.Tests.UtcNowTests;

public class SystemUtcNowProviderTests
{
    [Test]
    public void GetUtcNow_ShouldReturnCurrentUtcTime_WhenUsingSystemUtcNowProvider()
    {
        var systemProvider = new SystemUtcNowProvider();

        var dateTimeResult = systemProvider.GetUtcNow<DateTime>();
        dateTimeResult.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        var dateTimeOffsetResult = systemProvider.GetUtcNow<DateTimeOffset>();
        dateTimeOffsetResult.ShouldBe(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

        var dateOnlyResult = systemProvider.GetUtcNow<DateOnly>();
        dateOnlyResult.ShouldBe(DateOnly.FromDateTime(DateTime.UtcNow));
    }

    [Test]
    public void GetUtcNow_ShouldThrowNotSupportedException_WhenUsingUnsupportedType()
    {
        var systemProvider = new SystemUtcNowProvider();
        Should.Throw<NotSupportedException>(() => systemProvider.GetUtcNow<int>());
    }

    [Test, NotInParallel]
    public void EndToEndTest_WithSystemUtcNowProvider()
    {
        RuleContext.UseSystemUtcNow();
        var utcNowFromContext = RuleContext.UtcNow<DateTimeOffset>();
        utcNowFromContext.ShouldBe(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(10));

        // Check if the DateRules.Past rule correctly evaluates against the fixed UTC time
        var ruleResult = DateRules.WithinPast(TimeSpan.FromSeconds(1))(DateTimeOffset.UtcNow);
        ruleResult.IsValid.ShouldBeTrue();
    }
}