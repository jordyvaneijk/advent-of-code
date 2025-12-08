namespace Ved.AdventOfCode.Common.Helpers;

public interface IFileSystem
{
    Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default);
    Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);
    Stream OpenRead(string path, CancellationToken cancellationToken = default);
    Task<FileInfo> GetFileInfoAsync(string path, CancellationToken cancellationToken = default);
    IAsyncEnumerable<string> ReadLinesAsync(string path, CancellationToken cancellationToken = default);
}