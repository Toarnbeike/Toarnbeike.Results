using Toarnbeike.Results.Rules.Tolerances;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Context used for evaluating rules, such as tolerances and the current time.
/// </summary>
public sealed record ValidationContext(IToleranceProvider ToleranceProvider, TimeProvider TimeProvider)
{
    public static ValidationContext Default => 
        new(DefaultToleranceProvider.Instance, TimeProvider.System);
}