using Ved.AdventOfCode.Common.Models;

namespace AdventOfCode.Common.Tests.Models;

public class GridPos2dTests
{
    [Fact]
    public void Zero_ReturnsOrigin()
    {
        Assert.Equal(0, GridPos2d.Zero.Row);
        Assert.Equal(0, GridPos2d.Zero.Col);
    }

    [Fact]
    public void One_ReturnsOneOne()
    {
        Assert.Equal(1, GridPos2d.One.Row);
        Assert.Equal(1, GridPos2d.One.Col);
    }

    [Fact]
    public void DirectionConstants_AreCorrect()
    {
        Assert.Equal(new GridPos2d(0, 1), GridPos2d.Right);
        Assert.Equal(new GridPos2d(1, 0), GridPos2d.Down);
        Assert.Equal(new GridPos2d(0, -1), GridPos2d.Left);
        Assert.Equal(new GridPos2d(-1, 0), GridPos2d.Up);
        Assert.Equal(new GridPos2d(-1, 1), GridPos2d.RightUp);
        Assert.Equal(new GridPos2d(1, 1), GridPos2d.RightDown);
        Assert.Equal(new GridPos2d(-1, -1), GridPos2d.LeftUp);
        Assert.Equal(new GridPos2d(1, -1), GridPos2d.LeftDown);
    }

    [Fact]
    public void Operator_Addition_SubtractsAndAddsCorrectly()
    {
        var a = new GridPos2d(2, 3);
        var b = new GridPos2d(1, 1);
        Assert.Equal(new GridPos2d(3, 4), a + b);
        Assert.Equal(new GridPos2d(1, 2), a - b);
    }

    [Fact]
    public void Operator_MultiplicationAndAdditionWithInt_WorksCorrectly()
    {
        var a = new GridPos2d(2, 3);
        Assert.Equal(new GridPos2d(4, 6), a * 2);
        Assert.Equal(new GridPos2d(4, 5), a + 2);
        Assert.Equal(new GridPos2d(0, 1), a - 2);
    }

    [Fact]
    public void Operator_Negation_WorksCorrectly()
    {
        var a = new GridPos2d(2, -3);
        Assert.Equal(new GridPos2d(-2, 3), -a);
    }

    [Fact]
    public void IsInside_ReturnsTrueForValidPosition()
    {
        var pos = new GridPos2d(1, 1);
        Assert.True(pos.IsInside(3, 3));
    }

    [Fact]
    public void IsInside_ReturnsFalseForInvalidPosition()
    {
        var pos = new GridPos2d(-1, 0);
        Assert.False(pos.IsInside(3, 3));
        pos = new GridPos2d(3, 0);
        Assert.False(pos.IsInside(3, 3));
        pos = new GridPos2d(0, 3);
        Assert.False(pos.IsInside(3, 3));
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var pos = new GridPos2d(2, 3);
        Assert.Equal("(2, 3)", pos.ToString());
    }

    [Fact]
    public void AdjacentSide_ReturnsCorrectNeighbors()
    {
        var pos = new GridPos2d(1, 1);
        var neighbors = pos.AdjacentSide().ToArray();
        Assert.Contains(new GridPos2d(0, 1), neighbors);
        Assert.Contains(new GridPos2d(2, 1), neighbors);
        Assert.Contains(new GridPos2d(1, 0), neighbors);
        Assert.Contains(new GridPos2d(1, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void AdjacentDiag_ReturnsCorrectNeighbors()
    {
        var pos = new GridPos2d(1, 1);
        var neighbors = pos.AdjacentDiag().ToArray();
        Assert.Contains(new GridPos2d(0, 0), neighbors);
        Assert.Contains(new GridPos2d(0, 2), neighbors);
        Assert.Contains(new GridPos2d(2, 0), neighbors);
        Assert.Contains(new GridPos2d(2, 2), neighbors);
        Assert.Equal(4, neighbors.Length);
    }

    [Fact]
    public void AdjacentAll_ReturnsAllNeighbors()
    {
        var pos = new GridPos2d(1, 1);
        var neighbors = pos.AdjacentAll().ToArray();
        Assert.Equal(8, neighbors.Length);
    }

    [Fact]
    public void AdjacentSide_WithLengthAndBounds_ReturnsValidSequences()
    {
        var pos = new GridPos2d(1, 1);
        var sequences = pos.AdjacentSide(3, 3, 2).ToArray();
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(1, 2) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(2, 1) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(1, 0) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(0, 1) }));
        Assert.Equal(4, sequences.Length);
    }

    [Fact]
    public void AdjacentDiag_WithLengthAndBounds_ReturnsValidSequences()
    {
        var pos = new GridPos2d(1, 1);
        var sequences = pos.AdjacentDiag(3, 3, 2).ToArray();
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(0, 0) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(0, 2) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(2, 0) }));
        Assert.Contains(sequences, seq => seq.SequenceEqual(new[] { new GridPos2d(1, 1), new GridPos2d(2, 2) }));
        Assert.Equal(4, sequences.Length);
    }

    [Fact]
    public void AdjacentAll_WithLengthAndBounds_ReturnsValidSequences()
    {
        var pos = new GridPos2d(1, 1);
        var sequences = pos.AdjacentAll(3, 3, 2).ToArray();
        Assert.Equal(8, sequences.Length);
    }

    // [Fact]
    // public void Adjacent_WithLengthAndBounds_ExcludesOutOfBoundsSequences()
    // {
    //     var pos = new GridPos2d(0, 0);
    //     var sequences = pos.AdjacentSide(2, 2, 2).ToArray();
    //     Assert.Single(sequences);
    //     Assert.True(sequences[0].SequenceEqual(new[] { new GridPos2d(0, 0), new GridPos2d(0, 1) }));
    // }
}