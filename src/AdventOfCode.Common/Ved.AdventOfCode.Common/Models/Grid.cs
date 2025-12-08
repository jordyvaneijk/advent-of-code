using System.Text;

namespace Ved.AdventOfCode.Common.Models;

public class Grid<T> : IGrid
{
    public int Rows { get; }
    public int Cols { get; }

    private readonly T[][] _grid;

    public T this[int row, int col]
    {
        get => _grid[row][col];
        set => _grid[row][col] = value;
    }

    public T this[GridPos2d pos]
    {
        get => _grid[pos.Row][pos.Col];
        set => _grid[pos.Row][pos.Col] = value;
    }

    public Grid(Grid<T> grid)
    {
        Rows = grid.Rows;
        Cols = grid.Cols;
        _grid = new T[Rows][];
        for (int r = 0; r < Rows; r++)
        {
            _grid[r] = [..grid._grid[r]];
        }
    }

    public Grid(T[][] grid)
    {
        _grid = grid;
        Rows = grid.Length;
        Cols = grid[0].Length;
    }

    public Grid(GridPos2d size, T @default) : this(size.Row, size.Col, @default)
    {
    }

    public Grid(int rows, int cols, T @default)
    {
        _grid = new T[rows][];
        for (int r = 0; r < rows; r++)
        {
            _grid[r] = Enumerable.Repeat(@default, cols).ToArray();
        }

        Rows = rows;
        Cols = cols;
    }

    public bool Contains(GridPos2d pos)
    {
        return pos.IsInside(Rows, Cols);
    }

    public IEnumerable<GridItem<T>> Row(int row)
    {
        for (int col = 0; col < Cols; col++)
        {
            var pos = new GridPos2d(row, col);
            yield return new GridItem<T>(this[pos], pos);
        }
    }
    
    public IEnumerable<GridItem<T>> Column(int col)
    {
        for (int row = 0; row < Rows; row++)
        {
            var pos = new GridPos2d(row, col);
            yield return new GridItem<T>(this[pos], pos);
        }
    }
    
    public IEnumerable<GridItem<T>> Flatten()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                var pos = new GridPos2d(row, col);
                yield return new GridItem<T>(this[pos], pos);
            }
        }
    }

    public IEnumerable<GridItem<T>> VerticallyFlatten()
    {
        for (int col = 0; col < Cols; col++)
        {
            for (int row = 0; row < Rows; row++)
            {
                var pos = new GridPos2d(row, col);
                yield return new GridItem<T>(this[pos], pos);
            }
        }
    }

    public IEnumerable<GridItem<T>> AdjacentSide(GridItem<T> item)
    {
        return AdjacentSide(item.Position);
    }

    public IEnumerable<GridItem<T>> AdjacentDiag(GridItem<T> item)
    {
        return AdjacentDiag(item.Position);
    }

    public IEnumerable<GridItem<T>> AdjacentAll(GridItem<T> item)
    {
        return AdjacentAll(item.Position);
    }

    public IEnumerable<GridItem<T>> AdjacentSide(GridPos2d pos)
    {
        return Adjacent(pos.AdjacentSide());
    }

    public IEnumerable<GridItem<T>> AdjacentDiag(GridPos2d pos)
    {
        return Adjacent(pos.AdjacentDiag());
    }

    public IEnumerable<GridItem<T>> AdjacentAll(GridPos2d pos)
    {
        return Adjacent(pos.AdjacentAll());
    }

    private IEnumerable<GridItem<T>> Adjacent(IEnumerable<GridPos2d> adjacents)
    {
        return adjacents.Where(Contains)
            .Select(newPos => new GridItem<T>(this[newPos], newPos));
    }

    public string ToString(Func<T, object>? itemFormatter)
    {
        var res = new StringBuilder();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                res.Append(itemFormatter?.Invoke(this[i, j]) ?? this[i, j]);
            }

            res.AppendLine();
        }

        return res.ToString();
    }
    
    public override string ToString()
    {
        return ToString(null);
    }
}