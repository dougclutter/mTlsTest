# mTLS Test
This is a simple, command-line utility (e.g. - a console app) to test connectivity to a web server that is configured to use mutual TLS (e.g. - mTLS).
When connecting to an mTLS web server, you must provide a client certificate.
**mTlsTest** allows you to quickly check connecting to an mTLS web server both with and without a client certificate.

## Source Code
Here's a summary of the key source files:
- **Program.cs** (27 lines) - uses the System.CommandLine package to create a simple command line parser with built-in help.
- **GetUrlHandler.cs** (68 lines) - configure an `HttpClient` to perform an HTTP GET with a client certificate.

## Getting Started
### Download from github
You can download `mTlsTest.exe` from the [Releases](https://github.com/dougclutter/mTlsTest/releases) page.

### Building the EXE
1. Download the source code to your local drive.
1. Ensure you have the DotNet SDK v8.0.301 or later installed; it can be downloaded from https://dotnet.microsoft.com/en-us/download/dotnet/8.0.
1. Open a CMD window and change to the mTlsTest directory.  (e.g. - `CD /D C:\downloads\mTlsTest\mTlsTest`)
1. Run the following command: `dotnet publish -c Release -r win-x64 --sc`
1. This will create the `mTlsTest.exe` file in the **bin\Release\net8.0\win-x64\publish** folder.  You can run it from that folder or move it wherever you like.

## Command-line Options
```
Usage:
  mTlsTest [options]

Options:
  -u, --url <url> (REQUIRED)       The URL to access.
  -c, --certificate <certificate>  The client certificate to send when accessing the URL; this is usually a PFX file.
  -p, --password <password>        The password of the client certificate file.
  --version                        Show version information
  -?, -h, --help                   Show help and usage information
```

## Example Usage
This will send an HTTP GET request to https://company.com/api without any client certificate:  
```
mTlsTest -u https://company.com/api
```
---
This will load the certificate file **c:\cert.pfx** and send an HTTP GET request to https://company.com/api.
If the site requests a client certificate, the loaded certificate will be sent:
```
mTlsTest -u https://company.com/api -c c:\cert.pfx
```
---
This will load the certificate file **c:\cert.pfx** using the password **mySecretPassword** and send an HTTP GET request to https://company.com/api.
If the site requests a client certificate, the loaded certificate will be sent:
```
mTlsTest -u https://company.com/api -c c:\cert.pfx -p mySecretPassword
```
