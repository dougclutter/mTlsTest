namespace mTlsTest;

public class GetUrlHandlerTests
{
    const string validUrl = "https://httpstatuses.io/200";

    [Theory]
    [InlineData("company.com")]                 // Invalid because it is a relative URL
    [InlineData("http://company.com")]          // Invalid because it is HTTP not HTTPS
    public async Task InvalidUrlsReturn1(string url)
    {
        if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? uri))
            Assert.Fail();      // Only happens if provided URL is invalid

        var response = await GetUrlHandler.GetUrl(uri!, null, null);
        Assert.Equal(1, response);
    }
    [Fact]
    public async Task ValidUrlToNonExistentAddressReturns3()
    {
        var uri = new Uri("https://nosuchaddress.leo.state.mi.us");
        var response = await GetUrlHandler.GetUrl(uri!, null, null);
        Assert.Equal(3, response);
    }
    [Fact]
    public async Task NoCertificateGiven()
    {
        var uri = new Uri(validUrl);
        var response = await GetUrlHandler.GetUrl(uri!, null, null);
        Assert.Equal(0, response);
    }
    [Fact]
    public async Task FileDoesNotExist()
    {
        var uri = new Uri(validUrl);
        var file = new FileInfo("thisDoesNotExist.pfx");

        var response = await GetUrlHandler.GetUrl(uri!, file, null);

        Assert.Equal(2, response);
    }
    [Fact]
    public async Task InvalidFile()
    {
        var uri = new Uri(validUrl);
        using var resource = new ResourceToFile("NotAValidCert.pfx");

        var response = await GetUrlHandler.GetUrl(uri!, resource.FileInfo, null);

        Assert.Equal(2, response);
    }
    [Fact]
    public async Task ValidPfxButInvalidPassword()
    {
        var uri = new Uri(validUrl);
        using var resource = new ResourceToFile("TestCert.pfx");

        var response = await GetUrlHandler.GetUrl(uri!, resource.FileInfo, "this is NOT the password");

        Assert.Equal(2, response);
    }
    [Fact]
    public async Task ValidPfx()
    {
        var uri = new Uri(validUrl);
        using var resource = new ResourceToFile("TestCert.pfx");

        var response = await GetUrlHandler.GetUrl(uri!, resource.FileInfo, "pass");

        Assert.Equal(0, response);
    }
}
