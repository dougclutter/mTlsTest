# mTLS Test
This is a simple, command-line utility (e.g. - a console app) to test connectivity to a web server that is configured to use mutual TLS (e.g. - mTLS).
When connecting to an mTLS web server, you must provide a client certificate.
**mTlsTest** allows you to quickly check connecting to an mTLS web server both with and without a client certificate.

## Getting Started
1. Download the source code to your local drive.
1. Ensure you have the DotNet SDK v8.0.301 or later installed; it can be downloaded from https://dotnet.microsoft.com/en-us/download/dotnet/8.0.
1. In the mTlsTest folder which contains the mTlsTest.csproj file, enter the following command: **dotnet publish -c Release -r win-x64 --sc**
1. This will create the **mTlsTest.exe** file in the **bin\Release\net8.0\win-x64\publish** folder.  You can run it from that folder or move it wherever you like.

## Command-line Options
Usage:
  mTlsTest [options]

Options:
  -u, --url <url> (REQUIRED)       The URL to access.
  -c, --certificate <certificate>  The client certificate to send when accessing the URL; this is usually a PFX file.
  -p, --password <password>        The password of the client certificate file.
  --version                        Show version information
  -?, -h, --help                   Show help and usage information

## Example Usage
**mTlsTest -u https://company.com/api**

This will send an HTTP GET request to https://company.com/api without any client certificate.

**mTlsTest -u https://company.com/api -c c:\cert.pfx**

This will load the certificate file **c:\cert.pfx** and send an HTTP GET request to https://company.com/api.
If the site requests a client certificate, the loaded certificate will be sent.

**mTlsTest -u https://company.com/api -c c:\cert.pfx** -p mySecretPassword

This will load the certificate file **c:\cert.pfx** using the password **mySecretPassword** and send an HTTP GET request to https://company.com/api.
If the site requests a client certificate, the loaded certificate will be sent.
