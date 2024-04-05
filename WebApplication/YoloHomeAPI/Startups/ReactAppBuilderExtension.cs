using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.Extensions.FileProviders;

namespace YoloHomeAPI.Startups;

public static class ReactAppBuilderExtension
{
    
    public static void BuildReactApp(this IApplicationBuilder app)
    {
        string reactAppPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\yolohome-client\build");
        string hashFilePath = Path.Combine(reactAppPath, "hash.txt");

        // Calculate the hash of the current source files
        string currentHash = CalculateHash(Path.Combine(Directory.GetCurrentDirectory(), @"..\yolohome-client\src"));

        // Read the hash of the previous build
        string? previousHash = File.Exists(hashFilePath) ? File.ReadAllText(hashFilePath) : null;

        // If the hashes are different, rebuild the app
        if (currentHash != previousHash)
        {
            Console.WriteLine("Start building React app...");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true, // Capture error output
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\yolohome-client")
                }
            };

            process.Start();

            process.BeginOutputReadLine(); // Start reading standard output

            process.StartInfo.RedirectStandardError = true; // Ensure that standard error is redirected
            process.BeginErrorReadLine(); // Start reading error output

            process.StandardInput.WriteLine("npm run build");
            process.StandardInput.Close();

            process.WaitForExit();

            Console.WriteLine("React app built successfully.");

            // Save the hash of the current build
            File.WriteAllText(hashFilePath, currentHash);
        }
        else
        {
            Console.WriteLine("No changes detected. Skipping build.");
        }
    }

    private static string CalculateHash(string directoryPath)
    {
        // Use a hash algorithm of your choice
        using var sha256 = SHA256.Create();

        // Get all files in the directory and subdirectories
        var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                             .OrderBy(p => p).ToList();

        // Hash the content of each file and combine them
        foreach (var file in files)
        {
            byte[] content = File.ReadAllBytes(file);
            byte[] contentHash = sha256.ComputeHash(content);

            // Combine the current hash with the hash of the file content
            sha256.TransformBlock(contentHash, 0, contentHash.Length, contentHash, 0);
        }

        // Generate and return the final hash
        sha256.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
        return BitConverter.ToString(sha256.Hash ?? Array.Empty<byte>()).Replace("-", "").ToLowerInvariant();
    }

    public static void ServeReactApp(this IApplicationBuilder app)
    {
        // Serve the React app
        string reactAppPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\yolohome-client\build");
        app.UseFileServer(new FileServerOptions()
        {
            FileProvider = new PhysicalFileProvider(reactAppPath),
            RequestPath = "",
            EnableDefaultFiles = true,
        });

        // Serve the React app index.html for all non-API routes
        app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), builder =>
        {
            builder.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(reactAppPath, "index.html"));
            });
        });
    }
}