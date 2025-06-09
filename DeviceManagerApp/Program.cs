using DeviceManagerApp;
using DeviceManagerApp.Client.Pages;
using DeviceManagerApp.Components;
using DeviceManagerApp.Models;
using DeviceManagerApp.Services;
using DeviceManagerApp.Workers;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<APIManager>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DeviceManager>();
builder.Services.AddSingleton<APIManager>();
builder.Services.AddHostedService<DeviceMetricsWorker>();

var app = builder.Build();

// Resolve APIManager from the service container
var apiManager = app.Services.GetRequiredService<APIManager>();

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


app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DeviceManagerApp.Client._Imports).Assembly);

app.MapGet("/Hello", () => "Hello from server!"); // Til test

app.Run();
