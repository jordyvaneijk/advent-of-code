using Ved.AdventOfCode.Common.Models;

namespace AdventOfCode.Common.Tests.Models;

public class RangeTests
{
    [Fact]
    public void EnumerateReturnsAscendingSequenceForExclusiveEnd()
    {
        var range = new Range<int>(1, 4);

        Assert.Equal(new[] { 1, 2, 3 }, range.Enumerate());
    }

    [Fact]
    public void EnumerateReturnsEmptySequenceForEqualBounds()
    {
        var range = new Range<int>(3, 3);

        Assert.Empty(range.Enumerate());
    }

    [Fact]
    public void ContainsTreatsBoundsAsInclusive()
    {
        var range = new Range<int>(2, 5);

        Assert.True(range.Contains(2));
        Assert.True(range.Contains(5));
    }

    [Fact]
    public void LengthReflectsAbsoluteDifference()
    {
        var range = new Range<int>(5, 2);

        Assert.Equal(3, range.Length);
        Assert.False(range.Ascending);
    }

    [Fact]
    public void IntersectionReturnsOverlappingPortion()
    {
        var first = new Range<int>(0, 6);
        var second = new Range<int>(3, 9);

        var intersection = first.Intersection(second);

        Assert.Equal(3, intersection.Start);
        Assert.Equal(6, intersection.End);
        Assert.True(intersection.Ascending);
    }

    [Fact]
    public void IntersectionProducesDescendingRangeWhenDisjoint()
    {
        var first = new Range<int>(0, 3);
        var second = new Range<int>(5, 7);

        var intersection = first.Intersection(second);

        Assert.Equal(5, intersection.Start);
        Assert.Equal(3, intersection.End);
        Assert.False(intersection.Ascending);
    }

    [Fact]
    public void StartsBeforeReturnsTrueWhenStartIsSmaller()
    {
        var first = new Range<int>(0, 5);
        var second = new Range<int>(2, 8);

        Assert.True(first.StartsBefore(second));
        Assert.False(second.StartsBefore(first));
    }

    [Fact]
    public void EndsBeforeReflectsRelativeEndPositions()
    {
        var first = new Range<int>(0, 5);
        var second = new Range<int>(2, 8);

        Assert.True(first.EndsBefore(second));
        Assert.False(second.EndsBefore(first));
    }

    [Fact]
    public void IntersectsDetectsOverlap()
    {
        var first = new Range<int>(0, 5);
        var second = new Range<int>(3, 8);

        Assert.True(first.Intersects(second));
        Assert.True(second.Intersects(first));
    }

    [Fact]
    public void IntersectsReturnsFalseForSeparatedRanges()
    {
        var first = new Range<int>(0, 3);
        var second = new Range<int>(5, 9);

        Assert.False(first.Intersects(second));
        Assert.False(second.Intersects(first));
    }

    [Fact]
    public void UnionReturnsCombinedRangeForOverlap()
    {
        var first = new Range<int>(1, 4);
        var second = new Range<int>(3, 6);

        var union = first.Union(second);

        Assert.Equal(1, union.Start);
        Assert.Equal(6, union.End);
        Assert.True(union.Ascending);
    }

    [Fact]
    public void UnionThrowsWhenRangesDoNotIntersect()
    {
        var first = new Range<int>(0, 2);
        var second = new Range<int>(5, 7);

        Assert.Throws<ArgumentOutOfRangeException>(() => first.Union(second));
    }
}
