namespace Toarnbeike.Results.Guards;

/// <param name="isValid"> Indicates whether the guard condition succeeded. </param>
/// <param name="guardName"> The name of the guard that performed the evaluation. </param>
/// <param name="context"> Optional rule context associated with the guard. </param>
public class RuleEvaluation(bool isValid, string guardName, params (string key, object? value)[] context)
{
    public bool IsValid => isValid;
    public string GuardName => guardName;
    public (string key, object? value)[] Context => context;

    public static async Task<RuleEvaluation> FromTask(Task<bool> isValidTask, string guardName, params (string key, object? value)[] context)
    {
        var isValid = await isValidTask;
        return new RuleEvaluation(isValid, guardName, context);
    }

    public static async Task<RuleEvaluation> FromTaskInverted(Task<bool> isInvalidTask, string guardName, params (string key, object? value)[] context)
    {
        var isValid = !await isInvalidTask;
        return new RuleEvaluation(isValid, guardName, context);
    }
}