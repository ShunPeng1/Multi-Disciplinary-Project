using System.Diagnostics;
using Microsoft.Extensions.FileProviders;

namespace YoloHomeAPI.Startups;

public static class ReactAppBuilderExtension
{
    
    public static void BuildReactApp(this IApplicationBuilder app)
    {
        Console.WriteLine("Start building React app...");
        // Run npm run build before starting the application
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