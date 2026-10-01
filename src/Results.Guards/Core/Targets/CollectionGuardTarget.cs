namespace Toarnbeike.Results.Guards.Core.Targets;

internal sealed class CollectionGuardTarget<T> : ICollectionGuardTarget<T>
{
    public IGuardContext GuardContext { get; }
    public IList<T> Values { get; }
    public string Expression { get; }

    internal CollectionGuardTarget(IGuardContext context, IEnumerable<T> values, string expression) =>
        (GuardContext, Values, Expression) = (context, values.ToList(), expression);
}