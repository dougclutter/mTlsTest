namespace mTlsTest;

/// <summary>
/// Saves embedded resource to temp file and deletes it on Dispose.
/// </summary>
internal class ResourceToFile : IDisposable
{
    readonly string fileFullName = Path.GetTempFileName();

    public ResourceToFile(string resourceName)
    {
        using var resourceStream = GetType().Assembly.GetManifestResourceStream($"mTlsTest.Embedded.{resourceName}");
        using var fileStream = File.OpenWrite(fileFullName);
        resourceStream!.Seek(0, SeekOrigin.Begin);
        resourceStream!.CopyTo(fileStream);
    }
    public FileInfo FileInfo => new(fileFullName);
    public void Dispose() => File.Delete(fileFullName);
}
