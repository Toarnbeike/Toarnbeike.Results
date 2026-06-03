namespace Toarnbeike.Results.Guards.Implementations.Targets;

internal sealed class GuardTarget<T> : IGuardTarget<T>
{
    public IGuardContext GuardContext { get; }

    public T Value { get; }
    public string Expression { get; }

    internal GuardTarget(IGuardContext context, T value, string expression) =>
        (GuardContext, Value, Expression) = (context, value, expression);
}