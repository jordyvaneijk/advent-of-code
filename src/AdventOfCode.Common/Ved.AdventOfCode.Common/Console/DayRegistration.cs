using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ved.AdventOfCode.Common.Console;

/// <summary>
/// Represents a registered Advent of Code day with two executable parts.
/// </summary>
public sealed class DayRegistration
{
    public DayRegistration(
        int day,
        Func<CancellationToken, ValueTask<object?>> part1,
        Func<CancellationToken, ValueTask<object?>> part2,
        string? title = null)
    {
        if (day < 1)
            throw new ArgumentOutOfRangeException(nameof(day), "Day must be greater than or equal to 1.");

        Day = day;
        Part1 = part1 ?? throw new ArgumentNullException(nameof(part1));
        Part2 = part2 ?? throw new ArgumentNullException(nameof(part2));
        Title = string.IsNullOrWhiteSpace(title) ? $"Day {day:00}" : title;
    }

    public int Day { get; }
    public string Title { get; }
    public Func<CancellationToken, ValueTask<object?>> Part1 { get; }
    public Func<CancellationToken, ValueTask<object?>> Part2 { get; }
    public string Label => $"Day {Day:00} - {Title}";
    public override string ToString() => Label;
}

