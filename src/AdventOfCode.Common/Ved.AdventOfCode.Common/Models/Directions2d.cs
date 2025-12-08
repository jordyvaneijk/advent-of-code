namespace Ved.AdventOfCode.Common.Models;

public static class Directions2d
{
    public static readonly IReadOnlyList<GridPos2d> All =
    [
        GridPos2d.Right,
        GridPos2d.RightDown,
        GridPos2d.Down,
        GridPos2d.LeftDown,
        GridPos2d.Left,
        GridPos2d.LeftUp,
        GridPos2d.Up,
        GridPos2d.RightUp
    ];
    public static readonly IReadOnlyList<GridPos2d> Diag =
    [
        GridPos2d.RightDown,
        GridPos2d.LeftDown,
        GridPos2d.LeftUp,
        GridPos2d.RightUp
    ];

    public static readonly IReadOnlyList<GridPos2d> Side =
    [
        GridPos2d.Right,
        GridPos2d.Down,
        GridPos2d.Left,
        GridPos2d.Up
    ];

}