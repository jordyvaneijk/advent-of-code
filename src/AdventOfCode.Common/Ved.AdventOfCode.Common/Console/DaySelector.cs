using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleHost = System.Console;

namespace Ved.AdventOfCode.Common.Console;

internal sealed class DaySelector
{
    private readonly RunnerOptions _options;
    private readonly IReadOnlyList<DayRegistration> _registrations;

    public DaySelector(RunnerOptions options, IReadOnlyList<DayRegistration> registrations)
    {
        _options = options;
        _registrations = registrations;
    }

    public DayRegistration? SelectDay()
    {
        var minDay = Math.Max(_options.MinDay, _registrations.Min(r => r.Day));
        var maxDay = Math.Min(_options.MaxDay, _registrations.Max(r => r.Day));
        var selectedDay = minDay;
        var consecutiveDigits = string.Empty;
        var lastTyped = DateTime.UtcNow;

        Draw(selectedDay, minDay, maxDay);

        while (true)
        {
            var key = ConsoleHost.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedDay = Math.Max(minDay, selectedDay - 1);
                    Draw(selectedDay, minDay, maxDay);
                    break;
                case ConsoleKey.DownArrow:
                    selectedDay = Math.Min(maxDay, selectedDay + 1);
                    Draw(selectedDay, minDay, maxDay);
                    break;
                case ConsoleKey.Enter:
                    return _registrations.FirstOrDefault(r => r.Day == selectedDay);
                case ConsoleKey.Escape:
                    return null;
                default:
                    if (char.IsDigit(key.KeyChar))
                    {
                        if ((DateTime.UtcNow - lastTyped).TotalMilliseconds > 750)
                            consecutiveDigits = string.Empty;
                        lastTyped = DateTime.UtcNow;
                        consecutiveDigits += key.KeyChar;
                        if (int.TryParse(consecutiveDigits, out var typedDay)
                            && typedDay >= minDay
                            && typedDay <= maxDay)
                        {
                            selectedDay = typedDay;
                            Draw(selectedDay, minDay, maxDay);
                        }
                    }
                    break;
            }
        }
    }

    private void Draw(int selectedDay, int minDay, int maxDay)
    {
        ConsoleHost.WriteLine();
        ConsoleHost.WriteLine(_options.Prompt);

        for (var day = minDay; day <= maxDay; day++)
        {
            var registration = _registrations.FirstOrDefault(r => r.Day == day);
            var label = _options.DayLabelFactory?.Invoke(day) ?? $"Day {day:00}";

            if (registration == null)
            {
                ConsoleHost.ForegroundColor = _options.MenuDisabledColor;
                ConsoleHost.WriteLine($"  {label} (not registered)");
                ConsoleHost.ResetColor();
                continue;
            }

            var prefix = day == selectedDay ? ">" : " ";
            ConsoleHost.ForegroundColor = day == selectedDay ? _options.MenuSelectedColor : _options.MenuDefaultColor;
            ConsoleHost.WriteLine($"{prefix} {registration.Label}");
            ConsoleHost.ResetColor();
        }

        ConsoleHost.WriteLine();
        ConsoleHost.ForegroundColor = _options.MenuDefaultColor;
        ConsoleHost.WriteLine(_options.FooterText);
        ConsoleHost.ResetColor();
    }
}
