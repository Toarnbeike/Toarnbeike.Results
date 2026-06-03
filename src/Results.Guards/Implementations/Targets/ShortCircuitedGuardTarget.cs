namespace Toarnbeike.Results.Guards.Implementations.Targets;

internal sealed class ShortCircuitedGuardTarget<T> : IGuardTarget<T>
{
    public IGuardContext GuardContext { get; }

    public T Value => default!;
    public string Expression => string.Empty;

    internal ShortCircuitedGuardTarget(IGuardContext context) => GuardContext = context;
}