using Microsoft.Extensions.Logging;

namespace Toarnbeike.Results.Messaging.Tests.Helpers;

public static class TestLoggerAssertions
{
    public static void ShouldHaveMessage<T>(
        this TestLogger<T> logger,
        LogLevel level,
        params string[] fragments)
    {
        var hit = logger.Logs.Any(e =>
            e.Level == level &&
            fragments.All(f => e.Message.Contains(f)));

        if (!hit)
        {
            var dump = string.Join(Environment.NewLine,
                logger.Logs.Select(e => $"[{e.Level}] {e.Message}"));
            throw new ShouldAssertException(
                $"Expected a {level} log containing: {string.Join(", ", fragments)}{Environment.NewLine}Actual logs:{Environment.NewLine}{dump}");
        }
    }
}