# AdventOfCode.Common

A utility library for Advent of Code solutions, providing helpers for input handling, ranges, and grid-based problems.

## Classes

### Range<T>
Represents a numeric range with a start and end value. Supports intersection, containment, and enumeration.

**Example:**
```csharp
var range = new Range<int>(1, 10);
foreach (var i in range.Enumerate())
    Console.WriteLine(i); // 1 to 9
bool contains = range.Contains(5); // true
var intersection = range.Intersection(new Range<int>(5, 15)); // Range(5, 10)
```

### Grid<T>
A 2D grid structure for storing and accessing elements by position. Supports indexers by row/col and by GridPos2d.

**Example:**
```csharp
var grid = new Grid<int>(5, 5, 0); // 5x5 grid of zeros
grid[2, 3] = 42;
int value = grid[2, 3]; // 42
var pos = new GridPos2d(2, 3);
grid[pos] = 99;
```

### GridPos2d
Represents a position in a 2D grid with Row and Col coordinates. Provides helpers for directions and adjacency.

**Example:**
```csharp
var pos = new GridPos2d(2, 3);
var right = GridPos2d.Right; // (0, 1)
var down = GridPos2d.Down;   // (1, 0)
var neighbors = pos.AdjacentSide(); // Returns side-adjacent positions
```

### InputHelper
Provides methods for reading input files, lines, and streams. Supports async operations and custom file systems.

**Example:**
```csharp
// Read all lines from a file
var lines = await InputHelper.GetInput("input.txt");

// Read raw file content
var content = await InputHelper.GetRawInput("input.txt");

// Read file as a stream
using var stream = InputHelper.GetRawInputStream("input.txt");
```

## Installation
Add the project to your solution and reference `AdventOfCode.Common`.

## License
MIT
