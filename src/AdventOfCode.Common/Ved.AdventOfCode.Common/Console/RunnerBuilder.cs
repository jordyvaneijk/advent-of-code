using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ved.AdventOfCode.Common.Console;

/// <summary>
/// Fluent builder for configuring and running the Advent of Code console launcher.
/// </summary>
public sealed class RunnerBuilder
{
    private readonly List<DayRegistration> _registrations = new();
    private readonly RunnerOptions _options = new();

    public static RunnerBuilder Create() => new();

    public RunnerBuilder WithOptions(Action<RunnerOptions> configure)
    {
        configure?.Invoke(_options);
        return this;
    }

    public RunnerBuilder AddDay(
        int day,
        Func<CancellationToken, ValueTask<object?>> part1,
        Func<CancellationToken, ValueTask<object?>> part2,
        string? title = null)
    {
        _registrations.RemoveAll(r => r.Day == day);
        _registrations.Add(new DayRegistration(day, part1, part2, title));
        return this;
    }

    public RunnerBuilder AddDay(
        int day,
        Func<ValueTask<object?>> part1,
        Func<ValueTask<object?>> part2,
        string? title = null)
        => AddDay(day, _ => part1(), _ => part2(), title);

    public RunnerBuilder AddDay(
        int day,
        Func<object?> part1,
        Func<object?> part2,
        string? title = null)
        => AddDay(day, ct => new ValueTask<object?>(part1()), ct => new ValueTask<object?>(part2()), title);

    public RunnerBuilder AddDay(
        int day,
        Func<Task<object?>> part1,
        Func<Task<object?>> part2,
        string? title = null)
        => AddDay(day, async ct => await part1(), async ct => await part2(), title);

    public ConsoleRunner Build()
    {
        if (_registrations.Count == 0)
            throw new InvalidOperationException("At least one day must be registered before building the runner.");

        var ordered = _registrations
            .OrderBy(r => r.Day)
            .ToArray();

        return new ConsoleRunner(_options, ordered);
    }
}

