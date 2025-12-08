using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ved.AdventOfCode.Common.Console;

/// <summary>
/// Coordinates banner rendering, day selection, execution, and timing.
/// </summary>
public sealed class ConsoleRunner
{
    private readonly RunnerOptions _options;
    private readonly DayRegistration[] _registrations;

    internal ConsoleRunner(RunnerOptions options, DayRegistration[] registrations)
    {
        _options = options;
        _registrations = registrations;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        BannerRenderer.Render(_options);
        var selector = new DaySelector(_options, _registrations);

        while (true)
        {
            var registration = selector.SelectDay();
            if (registration == null)
                break;

            await ExecuteDayAsync(registration, cancellationToken);
            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to select another day, or Esc to exit...");

            var key = System.Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                break;
            BannerRenderer.Render(_options);
        }
    }

    private static async Task ExecuteDayAsync(DayRegistration registration, CancellationToken cancellationToken)
    {
        System.Console.WriteLine();
        System.Console.WriteLine($"Running {registration.Label}");
        System.Console.WriteLine(new string('-', registration.Label.Length + 8));

        await ExecutePartAsync("Part 1", registration.Part1, cancellationToken);
        await ExecutePartAsync("Part 2", registration.Part2, cancellationToken);
    }

    private static async Task ExecutePartAsync(
        string partLabel,
        Func<CancellationToken, ValueTask<object?>> part,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        object? result;
        try
        {
            result = await part(cancellationToken);
            sw.Stop();
        }
        catch (Exception ex)
        {
            sw.Stop();
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"{partLabel} failed after {sw.ElapsedMilliseconds}ms:");
            System.Console.ResetColor();
            System.Console.WriteLine(ex);
            return;
        }

        var elapsed = sw.Elapsed.TotalMilliseconds;
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{partLabel} ({elapsed:F2} ms)");
        System.Console.ResetColor();
        System.Console.WriteLine(result ?? "<no result>");
        System.Console.WriteLine();
    }
}

