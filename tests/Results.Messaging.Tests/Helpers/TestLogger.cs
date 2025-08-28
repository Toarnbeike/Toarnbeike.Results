using Microsoft.Extensions.Logging;

namespace Toarnbeike.Results.Messaging.Tests.Helpers;

public class TestLogger<T> : ILogger<T>
{
    private readonly List<(LogLevel Level, string Message)> _logs = new();

    public IReadOnlyList<(LogLevel Level, string Message)> Logs => _logs;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        _logs.Add((logLevel, message));
    }

    private class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new();
        public void Dispose() { }
    }
}