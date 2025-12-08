using System;
using System.Collections.Generic;

namespace Ved.AdventOfCode.Common.Console;

/// <summary>
/// Configuration for the Advent of Code console runner UI.
/// </summary>
public sealed class RunnerOptions
{
    private static readonly string[] DefaultBanner =
    {
        "    _       _                 _   ____        ____          _     ",
        "   / \\   __| |_   _____ _ __ | |_|  _ \\ _____/ ___|___   __| |___ ",
        "  / _ \\ / _` \\ \\ / / _ \\ '_ \\| __| | | / _ \\ |   / _ \\ / _` / __|",
        " / ___ \\ (_| |\\ V /  __/ | | | |_| |_| |  __/ |__| (_) | (_| \\__ \\",
        "/_/   \\_\\__,_| \\_/ \\___|_| |_|\\__|____/ \\___|\\____\\___/ \\__,_|___/"
    };

    private int _minDay = 1;
    private int _maxDay = 24;

    public int Year { get; set; } = DateTime.UtcNow.Year;
    public string BannerTitle { get; set; } = "Advent Of Code";
    public IReadOnlyList<string> BannerLines { get; set; } = DefaultBanner;
    public ConsoleColor BannerPrimaryColor { get; set; } = ConsoleColor.Cyan;
    public ConsoleColor BannerAccentColor { get; set; } = ConsoleColor.White;
    public bool ClearConsoleOnStart { get; set; } = true;
    public int MinDay
    {
        get => _minDay;
        set
        {
            _minDay = Math.Max(1, value);
            if (_maxDay < _minDay)
                _maxDay = _minDay;
        }
    }
    public int MaxDay
    {
        get => _maxDay;
        set => _maxDay = Math.Max(_minDay, value);
    }
    public string Prompt { get; set; } = "Select the day you want to run:";
    public string FooterText { get; set; } = "Use ↑/↓ or type a day number, Enter to run, Esc to quit.";
    public ConsoleColor MenuSelectedColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor MenuDefaultColor { get; set; } = ConsoleColor.Gray;
    public ConsoleColor MenuDisabledColor { get; set; } = ConsoleColor.DarkGray;
    public Func<int, string>? DayLabelFactory { get; set; }
}

