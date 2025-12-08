namespace Ved.AdventOfCode.Common.Helpers;

public class InputHelper
{
    private static IFileSystem _fileSystem = new DefaultFileSystem();
    public static void SetFileSystem(IFileSystem fileSystem) => _fileSystem = fileSystem;

    private static string GetFilePath(string fileName)
    {
        var outputDir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(outputDir, fileName);
    }

    public static async Task<List<string>> GetInput(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var lines = await _fileSystem.ReadAllLinesAsync(filePath,cancellationToken);
        return lines.ToList();
    }
    
    public static async Task<List<string>> GetInput(string fileName, string separator, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var content = await _fileSystem.ReadAllTextAsync(filePath,cancellationToken);
        return content
            .ReplaceLineEndings("")
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }
    
    public static async Task<string> GetRawInput(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var content = await _fileSystem.ReadAllTextAsync(filePath,cancellationToken);
        return content;
    }

    public static Stream GetRawInputStream(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var stream = _fileSystem.OpenRead(filePath,cancellationToken);
        return stream;
    }

    public static async IAsyncEnumerable<string> GetLines(string fileName, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        await foreach (var line in _fileSystem.ReadLinesAsync(filePath, cancellationToken))
            yield return line;
    }

    public static async Task<long> GetFileSize(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var info = await _fileSystem.GetFileInfoAsync(filePath, cancellationToken);
        return info.Length;
    }

    public static async Task<DateTime> GetLastModified(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = GetFilePath(fileName);
        var info = await _fileSystem.GetFileInfoAsync(filePath, cancellationToken);
        return info.LastWriteTimeUtc;
    }
}