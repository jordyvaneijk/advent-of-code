namespace Ved.AdventOfCode.Common.Helpers;

internal class DefaultFileSystem : IFileSystem
{
    public Task<string[]> ReadAllLinesAsync(string path,CancellationToken cancellationToken) => File.ReadAllLinesAsync(path, cancellationToken);
    public Task<string> ReadAllTextAsync(string path,CancellationToken cancellationToken) => File.ReadAllTextAsync(path,cancellationToken);
    public Stream OpenRead(string path,CancellationToken cancellationToken) => new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
    public Task<FileInfo> GetFileInfoAsync(string path,CancellationToken cancellationToken) => Task.FromResult(new FileInfo(path));
    public async IAsyncEnumerable<string> ReadLinesAsync(string path, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(OpenRead(path, cancellationToken));
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) yield break;
            var line = await reader.ReadLineAsync(cancellationToken);
            if (line is null) yield break;
            yield return line;
        }
    }
}