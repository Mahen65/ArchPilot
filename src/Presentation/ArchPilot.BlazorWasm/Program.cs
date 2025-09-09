using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ArchPilot.BlazorWasm;
using MudBlazor.Services;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to point to the API
var apiBaseUrl = "https://localhost:7002/";
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiBaseUrl)
});

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Fluxor for state management
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
    options.UseReduxDevTools();
#endif
});

// Configure PWA and caching only for production
#if RELEASE
// Register service worker for production caching only
builder.Services.AddServiceWorker("service-worker.js");
#endif

var host = builder.Build();

// Optional: Preload critical assemblies for better performance
await PreloadCriticalAssemblies(host);

await host.RunAsync();

// Method to preload critical assemblies
static async Task PreloadCriticalAssemblies(WebAssemblyHost host)
{
    try
    {
        // Preload assemblies that are likely to be used immediately
        var assemblies = new[]
        {
            typeof(MudBlazor.MudButton).Assembly,
            typeof(Fluxor.IDispatcher).Assembly,
            // Add other critical assemblies here
        };
        
        // This helps with faster startup by preloading assemblies
        await Task.Run(() =>
        {
            foreach (var assembly in assemblies)
            {
                _ = assembly.GetTypes();
            }
        });
    }
    catch
    {
        // Ignore preload errors - they're not critical
    }
}