namespace Toarnbeike.Results.Guards.Rules.Generated;

public static class DateOnlyCollectionRules
{
    public static ICollectionGuardRuleResult<DateOnly> OnOrAfter(this ICollectionGuardTarget<DateOnly> target, DateOnly min) =>
        target.EvaluateCollection(each => each.OnOrAfter(min));
}
