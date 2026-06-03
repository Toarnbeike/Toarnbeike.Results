using System.Globalization;
using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards.Implementations.GuardContext;

internal sealed class EnsureGuardContext(
    CultureInfo? culture = null,
    IToleranceProvider? toleranceProvider = null,
    TimeProvider? timeProvider = null) : GuardContextBase(toleranceProvider, timeProvider)
{
    public override IFailureMessageProvider FailureMessageProvider => MessageProviderFactory.GetForGuardFailures(culture);

    public override bool ShouldContinueExecution => Results.All(result => result.IsValid);

    protected override Result ToResult()
    {
        var firstFailure = Results.FirstOrDefault(result => !result.IsValid);
        return firstFailure is null
            ? Result.Success()
            : GuardFailure.FromGuardRuleResult(firstFailure, CreateMessage);
    }
}