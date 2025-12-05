using System.Numerics;

namespace AdventOfCode.Common.Models;

public class Range<T> where T : INumber<T>
{
    public T Start { get; }
    public T End { get; }
    public T Length { get; }
    public bool Ascending { get; }

    public Range(T start, T end)
    {
        Start = start;
        End = end;
        Length = T.Abs(End - Start);
        Ascending = End > Start;
    }

    public IEnumerable<T> Enumerate()
    {
        for (var i = Start; i < End; i++)
        {
            yield return i;
        }
    }

    public Range<T> Intersection(Range<T> other)
    {
        return new Range<T>(T.Max(Start, other.Start), T.Min(End, other.End));
    }

    public bool StartsBefore(Range<T> other)
    {
        return Start < other.Start;
    }

    public bool EndsBefore(Range<T> other)
    {
        return End < other.End;
    }

    public bool Intersects(Range<T> other)
    {
        var overlapStart = T.Max(Start, other.Start);
        var overlapEnd = T.Min(End, other.End);
        return overlapStart < overlapEnd;
    }

    public bool Contains(T item)
    {
        return Start <= item && item <= End;
    }

    public Range<T> Union(Range<T> other)
    {
        if(!Intersects(other))
        {
            throw new ArgumentOutOfRangeException("Cannot create union of non-intersecting ranges.");
        }
        
        return new Range<T>(T.Min(Start,other.Start), T.Max(End,other.End));
    }
}