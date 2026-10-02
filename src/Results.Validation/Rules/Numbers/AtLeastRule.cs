namespace Toarnbeike.Results.Validation.Rules.Numbers;

internal sealed class AtLeastRule<T>(T minimum) : IValidationRule<T>
    where T : IComparable<T>
{
    public RuleResult Validate(T value)
    {
        return value.CompareTo(minimum) < 0 
            ? RuleResult.Invalid($"must be at least {minimum}.") 
            : RuleResult.Valid();
    }
}
