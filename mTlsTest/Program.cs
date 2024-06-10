using mTlsTest;
using System.CommandLine;

var urlOption = new Option<Uri>(
    name: "--url",
    description: "The URL to access.")
{ IsRequired = true };
urlOption.AddAlias("-u");

var certOption = new Option<FileInfo?>(
    name: "--certificate",
    description: "The client certificate to send when accessing the URL; this is usually a PFX file.");
certOption.AddAlias("-c");

var passwordOption = new Option<string?>(
    name: "--password",
    description: "The password of the client certificate file.");
passwordOption.AddAlias("-p");

var rootCommand = new RootCommand("mTLS Test");
rootCommand.AddOption(urlOption);
rootCommand.AddOption(certOption);
rootCommand.AddOption(passwordOption);

rootCommand.SetHandler(GetUrlHandler.GetUrl, urlOption, certOption, passwordOption);

return await rootCommand.InvokeAsync(args);
