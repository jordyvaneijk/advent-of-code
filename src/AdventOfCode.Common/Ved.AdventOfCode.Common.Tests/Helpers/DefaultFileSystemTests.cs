using Ved.AdventOfCode.Common.Helpers;

namespace Ved.AdventOfCode.Common.Tests.Helpers;

public class DefaultFileSystemTests
{
    [Fact]
    public async Task ReadAllLinesAsync_ReturnsAllLines()
    {
        var path = Path.GetTempFileName();
        await File.WriteAllLinesAsync(path, new[] { "a", "b", "c" });
        var fs = new DefaultFileSystem();
        var lines = await fs.ReadAllLinesAsync(path, CancellationToken.None);
        Assert.Equal(new[] { "a", "b", "c" }, lines);
        File.Delete(path);
    }

    [Fact]
    public async Task ReadAllTextAsync_ReturnsFullContent()
    {
        var path = Path.GetTempFileName();
        await File.WriteAllTextAsync(path, "hello world");
        var fs = new DefaultFileSystem();
        var text = await fs.ReadAllTextAsync(path, CancellationToken.None);
        Assert.Equal("hello world", text);
        File.Delete(path);
    }

    [Fact]
    public void OpenRead_ReturnsReadableStream()
    {
        var path = Path.GetTempFileName();
        File.WriteAllText(path, "abc");
        var fs = new DefaultFileSystem();
        using var stream = fs.OpenRead(path, CancellationToken.None);
        Assert.True(stream.CanRead);
    }

    [Fact]
    public async Task GetFileInfoAsync_ReturnsFileInfo()
    {
        var path = Path.GetTempFileName();
        File.WriteAllText(path, "abc");
        var fs = new DefaultFileSystem();
        var info = await fs.GetFileInfoAsync(path, CancellationToken.None);
        Assert.Equal(path, info.FullName);
        Assert.True(info.Length > 0);
        File.Delete(path);
    }

    [Fact]
    public async Task ReadLinesAsync_ReturnsLinesOneByOne()
    {
        var path = Path.GetTempFileName();
        await File.WriteAllLinesAsync(path, new[] { "x", "y", "z" });
        var fs = new DefaultFileSystem();
        var result = new List<string>();
        await foreach (var line in fs.ReadLinesAsync(path))
            result.Add(line);
        Assert.Equal(new[] { "x", "y", "z" }, result);
        File.Delete(path);
    }

    [Fact]
    public async Task ReadLinesAsync_RespectsCancellationToken()
    {
        var path = Path.GetTempFileName();
        await File.WriteAllLinesAsync(path, new[] { "1", "2", "3" });
        var fs = new DefaultFileSystem();
        var cts = new CancellationTokenSource();
        var result = new List<string>();
        await foreach (var line in fs.ReadLinesAsync(path, cts.Token))
        {
            result.Add(line);
            cts.Cancel();
        }
        Assert.Single(result);
        File.Delete(path);
    }

    [Fact]
    public async Task ReadAllLinesAsync_ThrowsForMissingFile()
    {
        var fs = new DefaultFileSystem();
        await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await fs.ReadAllLinesAsync("notfound.txt", CancellationToken.None));
    }

    [Fact]
    public async Task ReadAllTextAsync_ThrowsForMissingFile()
    {
        var fs = new DefaultFileSystem();
        await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await fs.ReadAllTextAsync("notfound.txt", CancellationToken.None));
    }

    [Fact]
    public void OpenRead_ThrowsForMissingFile()
    {
        var fs = new DefaultFileSystem();
        Assert.Throws<FileNotFoundException>(() => fs.OpenRead("notfound.txt", CancellationToken.None));
    }

    [Fact]
    public async Task GetFileInfoAsync_ReturnsInfoForNonexistentFile()
    {
        var fs = new DefaultFileSystem();
        var info = await fs.GetFileInfoAsync("notfound.txt", CancellationToken.None);
        Assert.Equal("notfound.txt", info.Name);
    }
}