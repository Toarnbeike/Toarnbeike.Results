namespace Toarnbeike.Results.Validation.Rules;

public interface IValidationRule<in T>
{
    RuleResult Validate(T value);
}