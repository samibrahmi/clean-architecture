using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Récupérer la configuration des ports
var httpPort = builder.Configuration.GetValue<int>("ApplicationUrls:HttpPort", 5293);
var httpsPort = builder.Configuration.GetValue<int>("ApplicationUrls:HttpsPort", 7063);
var spaDevServerPort = builder.Configuration.GetValue<int>("ApplicationUrls:SpaDevServerPort", 3000);

// Configurer les URLs de l'application explicitement
builder.WebHost.ConfigureKestrel(options => {
    options.ListenAnyIP(httpPort, listenOptions => {
        // Configuration HTTP
    });
    
    options.ListenAnyIP(httpsPort, listenOptions => {
        listenOptions.UseHttps();
    });
});

// Définir les URLs explicitement
builder.WebHost.UseUrls($"https://localhost:{httpsPort}", $"http://localhost:{httpPort}");

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "clientapp/dist"; // Vite builds to 'dist' by default
});

// Configure HTTPS redirection with port from configuration
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = httpsPort;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// Configure SPA middleware
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "clientapp";
    
    if (app.Environment.IsDevelopment())
    {
        // Simplified development server integration with port from configuration
        spa.UseProxyToSpaDevelopmentServer($"http://localhost:{spaDevServerPort}");
        
        // Start Vite in a separate process with port from configuration
        var clientAppPath = Path.Combine(app.Environment.ContentRootPath, "clientapp");
        app.Logger.LogInformation("Starting Vite development server on port {Port}...", spaDevServerPort);
        
        var processInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c npm run start -- --port {spaDevServerPort}",
            WorkingDirectory = clientAppPath,
            UseShellExecute = true, // Don't capture output - let it show in its own window
        };
        
        Process.Start(processInfo);
    }
});

app.Run();
