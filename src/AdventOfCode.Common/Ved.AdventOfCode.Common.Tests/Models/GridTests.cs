using Ved.AdventOfCode.Common.Models;

namespace AdventOfCode.Common.Tests.Models;

public class GridTests
{
    [Fact]
    public void Grid_CanBeInitializedWith2DArray_DefaultValuesSet()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        Assert.Equal(2, grid.Rows);
        Assert.Equal(2, grid.Cols);
        Assert.Equal(1, grid[0, 0]);
        Assert.Equal(4, grid[1, 1]);
    }

    [Fact]
    public void Grid_CanBeInitializedWithSizeAndDefaultValue_AllCellsSetToDefault()
    {
        var grid = new Grid<int>(3, 2, 7);
        for (int r = 0; r < 3; r++)
        for (int c = 0; c < 2; c++)
            Assert.Equal(7, grid[r, c]);
    }

    [Fact]
    public void Grid_Indexer_SetAndGetValue_WorksCorrectly()
    {
        var grid = new Grid<int>(2, 2, 0);
        grid[1, 1] = 42;
        Assert.Equal(42, grid[1, 1]);
    }

    [Fact]
    public void Grid_IndexerWithGridPos2d_SetAndGetValue_WorksCorrectly()
    {
        var grid = new Grid<int>(2, 2, 0);
        var pos = new GridPos2d(0, 1);
        grid[pos] = 99;
        Assert.Equal(99, grid[pos]);
    }

    [Fact]
    public void Grid_Contains_ReturnsTrueForValidPosition()
    {
        var grid = new Grid<int>(2, 2, 0);
        Assert.True(grid.Contains(new GridPos2d(1, 1)));
    }

    [Fact]
    public void Grid_Contains_ReturnsFalseForInvalidPosition()
    {
        var grid = new Grid<int>(2, 2, 0);
        Assert.False(grid.Contains(new GridPos2d(2, 2)));
        Assert.False(grid.Contains(new GridPos2d(-1, 0)));
    }

    [Fact]
    public void Grid_Row_ReturnsCorrectItems()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var row = grid.Row(1).ToArray();
        Assert.Equal(2, row.Length);
        Assert.Equal(3, row[0].Value);
        Assert.Equal(4, row[1].Value);
    }

    [Fact]
    public void Grid_Column_ReturnsCorrectItems()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var col = grid.Column(0).ToArray();
        Assert.Equal(2, col.Length);
        Assert.Equal(1, col[0].Value);
        Assert.Equal(3, col[1].Value);
    }

    [Fact]
    public void Grid_Flatten_ReturnsAllItemsInRowMajorOrder()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var flat = grid.Flatten().Select(i => i.Value).ToArray();
        Assert.Equal(new[] { 1, 2, 3, 4 }, flat);
    }

    [Fact]
    public void Grid_VerticallyFlatten_ReturnsAllItemsInColumnMajorOrder()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var flat = grid.VerticallyFlatten().Select(i => i.Value).ToArray();
        Assert.Equal(new[] { 1, 3, 2, 4 }, flat);
    }

    [Fact]
    public void Grid_ToString_UsesDefaultFormatting()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var str = grid.ToString();
        Assert.Contains("1", str);
        Assert.Contains("2", str);
        Assert.Contains("3", str);
        Assert.Contains("4", str);
    }

    [Fact]
    public void Grid_ToString_UsesCustomFormatter()
    {
        int[][] arr = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
        var grid = new Grid<int>(arr);
        var str = grid.ToString(i => $"[{i}]");
        Assert.Contains("[1]", str);
        Assert.Contains("[2]", str);
    }

    [Fact]
    public void Grid_AdjacentSide_ReturnsCorrectNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var pos = new GridPos2d(1, 1);
        var neighbors = grid.AdjacentSide(pos).Select(i => i.Position).ToArray();
        Assert.Contains(new GridPos2d(0, 1), neighbors);
        Assert.Contains(new GridPos2d(2, 1), neighbors);
        Assert.Contains(new GridPos2d(1, 0), neighbors);
        Assert.Contains(new GridPos2d(1, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void Grid_AdjacentDiag_ReturnsCorrectNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var pos = new GridPos2d(1, 1);
        var neighbors = grid.AdjacentDiag(pos).Select(i => i.Position).ToArray();
        Assert.Contains(new GridPos2d(0, 0), neighbors);
        Assert.Contains(new GridPos2d(0, 2), neighbors);
        Assert.Contains(new GridPos2d(2, 0), neighbors);
        Assert.Contains(new GridPos2d(2, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void Grid_AdjacentAll_ReturnsAllNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var pos = new GridPos2d(1, 1);
        var neighbors = grid.AdjacentAll(pos).Select(i => i.Position).ToArray();
        Assert.Equal(8, neighbors.Length);
    }

    [Fact]
    public void Grid_AdjacentSide_EdgeCell_ReturnsOnlyValidNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var pos = new GridPos2d(0, 0);
        var neighbors = grid.AdjacentSide(pos).Select(i => i.Position).ToArray();
        Assert.Contains(new GridPos2d(0, 1), neighbors);
        Assert.Contains(new GridPos2d(1, 0), neighbors);
        Assert.Equal(2, neighbors.Length);
    }

    [Fact]
    public void Grid_CloneConstructor_CreatesDeepCopy()
    {
        var original = new Grid<int>(2, 2, 5);
        original[0, 0] = 10;
        var clone = new Grid<int>(original);
        Assert.Equal(10, clone[0, 0]);
        clone[0, 0] = 99;
        Assert.NotEqual(original[0, 0], clone[0, 0]);
    }

    [Fact]
    public void Grid_GridPos2dConstructor_CreatesGridWithCorrectSizeAndDefaultValue()
    {
        var size = new GridPos2d(2, 3);
        var grid = new Grid<int>(size, 5);
        Assert.Equal(2, grid.Rows);
        Assert.Equal(3, grid.Cols);
        for (int r = 0; r < 2; r++)
            for (int c = 0; c < 3; c++)
                Assert.Equal(5, grid[r, c]);
    }

    [Fact]
    public void Grid_AdjacentSide_WithGridItem_ReturnsCorrectNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var item = new GridItem<int>(0, new GridPos2d(1, 1));
        var neighbors = grid.AdjacentSide(item).Select(i => i.Position).ToArray();
        Assert.Contains(new GridPos2d(0, 1), neighbors);
        Assert.Contains(new GridPos2d(2, 1), neighbors);
        Assert.Contains(new GridPos2d(1, 0), neighbors);
        Assert.Contains(new GridPos2d(1, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void Grid_AdjacentDiag_WithGridItem_ReturnsCorrectNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var item = new GridItem<int>(0, new GridPos2d(1, 1));
        var neighbors = grid.AdjacentDiag(item).Select(i => i.Position).ToArray();
        Assert.Contains(new GridPos2d(0, 0), neighbors);
        Assert.Contains(new GridPos2d(0, 2), neighbors);
        Assert.Contains(new GridPos2d(2, 0), neighbors);
        Assert.Contains(new GridPos2d(2, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void Grid_AdjacentAll_WithGridItem_ReturnsAllNeighbors()
    {
        var grid = new Grid<int>(3, 3, 0);
        var item = new GridItem<int>(0, new GridPos2d(1, 1));
        var neighbors = grid.AdjacentAll(item).Select(i => i.Position).ToArray();
        Assert.Equal(8, neighbors.Length);
    }
}