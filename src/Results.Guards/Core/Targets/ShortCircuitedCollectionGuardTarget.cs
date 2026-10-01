namespace Toarnbeike.Results.Guards.Core.Targets;

internal sealed class ShortCircuitedCollectionGuardTarget<T> : ICollectionGuardTarget<T>
{
    public IGuardContext GuardContext { get; }
    public IList<T> Values => [];
    public string Expression => string.Empty;
    internal ShortCircuitedCollectionGuardTarget(IGuardContext context) => GuardContext = context;
}