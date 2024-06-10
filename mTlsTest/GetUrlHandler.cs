using System.Security.Cryptography.X509Certificates;

namespace mTlsTest;

public static class GetUrlHandler
{
    public static async Task<int> GetUrl(Uri url, FileInfo? certificateFileInfo, string? certificatePassword)
    {
        if (!url.IsAbsoluteUri || !url.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase)) 
        {
            Console.WriteLine("URL is not a valid web site address (e.g. - https://mysite.com/page); URLs for mTLS must use HTTPS.");
            return 1;
        }
        if (!TryGetCertificate(certificateFileInfo, certificatePassword, out var certificate))
        {
            return 2;
        }

        var handler = new HttpClientHandler();
        if (certificate != null)
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(certificate);
        }    
        var client = new HttpClient(handler);

        try
        {
            var response = await client.GetAsync(url);
            var successOrFail = response.IsSuccessStatusCode ? "Success" : "Failed";
            Console.WriteLine($"{successOrFail}!  HTTP GET returned {response.StatusCode} ({(int)response.StatusCode}).");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"HTTP GET failed: {ex.Message}");
            return 3;
        }
    }
    static bool TryGetCertificate(FileInfo? certificateFileInfo, string? certificatePassword, out X509Certificate2? certificate)
    {
        if (certificateFileInfo == null)
        {
            // User didn't provide a certificate path so we return a null.
            certificate = null;
            return true;
        }
        if (!certificateFileInfo.Exists)
        {
            // User provided an invalid path.
            Console.WriteLine("Certificate file not found.");
            certificate = null;
            return false;
        }
        try
        {
            certificate = new X509Certificate2(certificateFileInfo.FullName, certificatePassword);
            Console.WriteLine("Certificate loaded successfully.");
            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Unable to load certificate: {ex.Message}");
            certificate = null;
            return false;
        }
    }
}
