using System;

namespace Ved.AdventOfCode.Common.Console;

internal static class BannerRenderer
{
    public static void Render(RunnerOptions options)
    {
        if (options.ClearConsoleOnStart)
            System.Console.Clear();

        var previousColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = options.BannerPrimaryColor;

        foreach (var line in options.BannerLines)
        {
            System.Console.WriteLine(line);
        }

        System.Console.ForegroundColor = options.BannerAccentColor;
        System.Console.WriteLine();
        System.Console.WriteLine($"{options.BannerTitle} {options.Year}");
        System.Console.WriteLine(new string('-', options.BannerTitle.Length + 1 + options.Year.ToString().Length));
        System.Console.ForegroundColor = previousColor;
        System.Console.WriteLine();
    }
}

