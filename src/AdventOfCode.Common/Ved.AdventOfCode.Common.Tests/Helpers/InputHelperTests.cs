// using Ved.AdventOfCode.Common.Helpers;
// using NSubstitute;
//
// namespace Ved.AdventOfCode.Common.Tests.Helpers;
//
// public class InputHelperTests
// {
//     [Fact]
//     public async Task GetInput_ReturnsAllLines()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         mockFS.ReadAllLinesAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Task.FromResult(new[] { "a", "b" }));
//         InputHelper.SetFileSystem(mockFS);
//         var result = await InputHelper.GetInput("file.txt");
//         Assert.Equal(new[] { "a", "b" }, result);
//     }
//
//     [Fact]
//     public async Task GetInput_WithSeparator_SplitsContentCorrectly()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         mockFS.ReadAllTextAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Task.FromResult("a|b|c"));
//         InputHelper.SetFileSystem(mockFS);
//         var result = await InputHelper.GetInput("file.txt", "|");
//         Assert.Equal(new[] { "a", "b", "c" }, result);
//     }
//
//     [Fact]
//     public async Task GetRawInput_ReturnsRawContent()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         mockFS.ReadAllTextAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Task.FromResult("rawdata"));
//         InputHelper.SetFileSystem(mockFS);
//         var result = await InputHelper.GetRawInput("file.txt");
//         Assert.Equal("rawdata", result);
//     }
//
//     [Fact]
//     public void GetRawInputStream_ReturnsStream()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         var ms = new MemoryStream();
//         mockFS.OpenRead("file.txt", Arg.Any<CancellationToken>()).Returns(ms);
//         InputHelper.SetFileSystem(mockFS);
//         var result = InputHelper.GetRawInputStream("file.txt");
//         Assert.Same(ms, result);
//     }
//
//     [Fact]
//     public async Task GetLines_YieldsLinesAsync()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         async IAsyncEnumerable<string> Lines()
//         {
//             yield return "x";
//             yield return "y";
//         }
//         mockFS.ReadLinesAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Lines());
//         InputHelper.SetFileSystem(mockFS);
//         var result = new List<string>();
//         await foreach (var line in InputHelper.GetLines("file.txt"))
//             result.Add(line);
//         Assert.Equal(new[] { "x", "y" }, result);
//     }
//
//     [Fact]
//     public async Task GetFileSize_ReturnsFileSize()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         var fi = new FileInfo(Path.GetTempFileName());
//         mockFS.GetFileInfoAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Task.FromResult(fi));
//         InputHelper.SetFileSystem(mockFS);
//         var result = await InputHelper.GetFileSize("file.txt");
//         Assert.Equal(fi.Length, result);
//     }
//
//     [Fact]
//     public async Task GetLastModified_ReturnsLastWriteTimeUtc()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         var fi = new FileInfo(Path.GetTempFileName());
//         mockFS.GetFileInfoAsync("file.txt", Arg.Any<CancellationToken>()).Returns(Task.FromResult(fi));
//         InputHelper.SetFileSystem(mockFS);
//         var result = await InputHelper.GetLastModified("file.txt");
//         Assert.Equal(fi.LastWriteTimeUtc, result);
//     }
//
//     [Fact]
//     public async Task GetInput_ThrowsForMissingFile()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         mockFS.ReadAllLinesAsync("missing.txt", Arg.Any<CancellationToken>()).Returns<Task<string[]>>(x => throw new FileNotFoundException());
//         InputHelper.SetFileSystem(mockFS);
//         await Assert.ThrowsAsync<FileNotFoundException>(async () => await InputHelper.GetInput("missing.txt"));
//     }
//
//     [Fact]
//     public async Task GetRawInput_ThrowsForMissingFile()
//     {
//         var mockFS = Substitute.For<IFileSystem>();
//         mockFS.ReadAllTextAsync("missing.txt", Arg.Any<CancellationToken>()).Returns<Task<string>>(x => throw new FileNotFoundException());
//         InputHelper.SetFileSystem(mockFS);
//         await Assert.ThrowsAsync<FileNotFoundException>(async () => await InputHelper.GetRawInput("missing.txt"));
//     }
// }