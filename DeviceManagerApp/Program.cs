using DeviceManagerApp.Client.Pages;
using DeviceManagerApp.Components;
using DeviceManagerApp.Models;
using DeviceManagerApp;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<APIManager>();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapMetrics(); // Eksponerer /metrics endpoint

// for testing:
var testDevices = new List<Device>
{
    new Device
    {
        Id = "1",
        IpAddress = "192.168.1.10",
        Status = "Streaming",
        UpdatedAt = DateTime.UtcNow,
        Config = null
    },
    new Device
    {
        Id = "2",
        IpAddress = "192.168.1.11",
        Status = "Offline",
        UpdatedAt = DateTime.UtcNow,
        Config = null
    },
    new Device
    {
        Id = "3",
        IpAddress = "192.168.1.12",
        Status = "Streaming",
        UpdatedAt = DateTime.UtcNow,
        Config = null
    }
};
DeviceStatusToPrometheusPortal.UpdateMetrics(testDevices);




app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DeviceManagerApp.Client._Imports).Assembly);

app.MapGet("/Hello", () => "Hello from server!"); // Til test

app.Run();
